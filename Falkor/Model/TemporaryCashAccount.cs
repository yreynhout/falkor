using System;
using Falkor.Messages;

namespace Falkor.Model
{
  public class TemporaryCashAccount : IAggregateRootEntity
  {
    public TemporaryCashAccountId Id => _temporaryCashAccountId;

    public TemporaryCashAccount(TemporaryCashAccountId accountId, OwnerId ownerId) : this()
    {
      Apply(new TemporaryCashAccountOpened(accountId, ownerId));
    }

    public TemporaryCashAccount TransferTo(TemporaryCashAccountId transferAccountId)
    {
      ThrowIfClosed();

      var account = new TemporaryCashAccount();
      account.Apply(new TemporaryCashAccountTransfered(transferAccountId, _temporaryCashAccountId, _ownerId, _balance, CreateTransferToken()));
      return account;
    }

    private TransferToken CreateTransferToken()
    {
      using(var hash = System.Security.Cryptography.MD5.Create())
      {
        var input = new byte[16 + 4];
        Array.Copy(_temporaryCashAccountId.ToGuid().ToByteArray(), input, 16);
        Array.Copy(BitConverter.GetBytes(_revision), 0, input, 16, 4);
        return new TransferToken(hash.ComputeHash(input));
      }
    }

    public void Debit(double amount)
    {
      ThrowIfClosed();

      if ((_balance - amount) < 0.0)
        throw new InsufficientBalanceException(_temporaryCashAccountId);

      Apply(new TemporaryCashAccountDebited(_temporaryCashAccountId, amount));
    }

    public void Credit(double amount)
    {
      ThrowIfClosed();

      Apply(new TemporaryCashAccountCredited(_temporaryCashAccountId, amount));
    }

    public void Close(TemporaryCashAccount transferAccount)
    {
      ThrowIfClosed();

      if(!transferAccount._transferToken.Equals(CreateTransferToken()))
      {
        throw new TemporaryCashAccountTransferTokenMismatchException(_temporaryCashAccountId, transferAccount._temporaryCashAccountId);
      }
      Apply(new TemporaryCashAccountClosed(_temporaryCashAccountId, transferAccount._temporaryCashAccountId));
    }

    private void ThrowIfClosed()
    {
      if(_transferedToTemporaryCashAccountId.HasValue)
      {
        throw new TemporaryCashAccountClosedException(_temporaryCashAccountId, _transferedToTemporaryCashAccountId.Value);
      }
    }

    private TemporaryCashAccountId _temporaryCashAccountId;
    private OwnerId _ownerId;
    private int _revision;
    private double _balance;
    private TransferToken _transferToken;
    private TemporaryCashAccountId? _transferedToTemporaryCashAccountId;

    public static readonly Func<TemporaryCashAccount> Factory = () => new TemporaryCashAccount();
    private TemporaryCashAccount()
    {
      _router.Configure<TemporaryCashAccountOpened>(m =>
      {
        _temporaryCashAccountId = new TemporaryCashAccountId(m.TemporaryCashAccountId);
        _ownerId = new OwnerId(m.OwnerId);
        _balance = 0.0;
        _transferToken = new TransferToken(new byte[0]);
        _revision = 0;
        _transferedToTemporaryCashAccountId = null;
      });
      _router.Configure<TemporaryCashAccountTransfered>(m =>
      {
        _temporaryCashAccountId = new TemporaryCashAccountId(m.NewTemporaryCashAccountId);
        _ownerId = new OwnerId(m.OwnerId);
        _balance = m.Balance;
        _transferToken = new TransferToken(m.TransferToken);
        _revision = 0;
        _transferedToTemporaryCashAccountId = null;
      });
      _router.Configure<TemporaryCashAccountDebited>(m =>
      {
        _balance -= m.Amount;

        _revision += 1;
      });
      _router.Configure<TemporaryCashAccountCredited>(m =>
      {
        _balance += m.Amount;

        _revision += 1;
      });
      _router.Configure<TemporaryCashAccountClosed>(m =>
      {
        _transferedToTemporaryCashAccountId = new TemporaryCashAccountId(m.NewTemporaryCashAccountId);

        _revision += 1;
      });
    }

    private readonly Router _router = new Router();
    private readonly Recorder _recorder = new Recorder();
    Router IAggregateRootEntity.Router => _router;
    Recorder IAggregateRootEntity.Recorder => _recorder;
    private void Apply(object record)
    {
      _router.Route(record);
      _recorder.Record(record);
    }
  }
}