using System;
using System.Collections.Generic;
using Falkor.EventStore;
using Falkor.Model;
using Falkor.Messages;

namespace Falkor.Application
{
  public class TemporaryCashAccountRepository : ITemporaryCashAccountRepository
  {
    private readonly IStreamReader _reader;
    private readonly List<TemporaryCashAccount> _unitOfWork;

    public TemporaryCashAccountRepository(IStreamReader reader)
    {
      if(reader == null)
        throw new ArgumentNullException(nameof(reader));
      _reader = reader;
      _unitOfWork = new List<TemporaryCashAccount>();
    }

    public TemporaryCashAccount[] UnitOfWork => _unitOfWork.ToArray();

    public TemporaryCashAccount GetMostRecent(TemporaryCashAccountId id)
    {
      var accountId = FindMostRecentAccount(id);
      var root = _unitOfWork.Find(instance => instance.Id == accountId);
      if (root == null)
      {
        root = TemporaryCashAccount.Factory();
        root.RestoreFrom(_reader.ReadForward(accountId.ToString()));
        _unitOfWork.Add(root);
      }
      return root;
    }

    public TemporaryCashAccount Get(TemporaryCashAccountId id)
    {
      var root = _unitOfWork.Find(instance => instance.Id == id);
      if (root == null)
      {
        root = TemporaryCashAccount.Factory();
        root.RestoreFrom(_reader.ReadForward(id.ToString()));
        _unitOfWork.Add(root);
      }
      return root;
    }

    private TemporaryCashAccountId FindMostRecentAccount(TemporaryCashAccountId id)
    {
      //First we walk the chain of all closed accounts right up until we find a confirmed, non-closed account
      bool closed;
      var nextAccountId = id;
      do
      {
        closed = false;
        using(var stream = _reader.ReadBackward(nextAccountId.ToString()).GetEnumerator())
        {
          var moved = stream.MoveNext();
          if (moved)
          {
            if(stream.Current is TemporaryCashAccountClosed)
            {
              nextAccountId = new TemporaryCashAccountId(((TemporaryCashAccountClosed)stream.Current).NewTemporaryCashAccountId);
              closed = true;
            }
          }
        }
      } while (closed);
      return nextAccountId == id ? FindOpenOrConfirmedTransferAccount(id) : nextAccountId;
    }

    private TemporaryCashAccountId FindOpenOrConfirmedTransferAccount(TemporaryCashAccountId id)
    {
      //Second we verify that transfer account is actually confirmed.
      //If not, we do our best to guarantee the proper account is returned.
      using(var stream = _reader.ReadForward(id.ToString()).GetEnumerator())
      {
        var moved = stream.MoveNext();
        if (moved)
        {
          if(stream.Current is TemporaryCashAccountTransfered)
          {
            using(var oldStream = _reader.ReadBackward(((TemporaryCashAccountTransfered)stream.Current).OldTemporaryCashAccountId.ToString("N")).GetEnumerator())
            {
              var oldMoved = stream.MoveNext();
              if(oldMoved)
              {
                if(oldStream.Current is TemporaryCashAccountClosed)
                {
                  return new TemporaryCashAccountId(((TemporaryCashAccountClosed)oldStream.Current).NewTemporaryCashAccountId);
                }
                else
                {
                  return new TemporaryCashAccountId(((TemporaryCashAccountTransfered)stream.Current).OldTemporaryCashAccountId);
                }
              }
              else
              {
                //Illogical
                return new TemporaryCashAccountId(((TemporaryCashAccountTransfered)stream.Current).OldTemporaryCashAccountId);
              }
            }
          }
          else if(stream.Current is TemporaryCashAccountOpened)
          {
            return id;
          }
          else
          {
            //Illogical
            return id;
          }
        }
        else
        {
          //Illogical
          return id;
        }
      }
    }

    public void Add(TemporaryCashAccount instance)
    {
      if (instance == null) throw new ArgumentNullException(nameof(instance));
      _unitOfWork.Add(instance);
    }
  }
}