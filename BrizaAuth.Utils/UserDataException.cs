using System.Runtime.Serialization;

namespace BrizaAuth.Utils
{
  public class UserDataException : Exception
  {

    public object ResponseObject { get; }

    public UserDataException(string message) : base(message) { ResponseObject = new string[0]; }
  }
}
