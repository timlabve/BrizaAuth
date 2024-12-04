using System.Runtime.Serialization;

namespace Brizaapp.Utils
{
  public class UserDataException : Exception
  {

    public object ResponseObject { get; }

    public UserDataException(string message) : base(message) { ResponseObject = new string[0]; }
  }
}
