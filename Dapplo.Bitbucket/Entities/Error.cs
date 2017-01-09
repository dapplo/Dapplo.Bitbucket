using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities {
	[DataContract]
	public class Error {
		[DataMember(Name = "context", EmitDefaultValue = false)]
		public string Context {
			get;
			set;
		}
		[DataMember(Name = "message", EmitDefaultValue = false)]
		public string Message {
			get;
			set;
		}
		[DataMember(Name = "exceptionName", EmitDefaultValue = false)]
		public string ExceptionName {
			get;
			set;
		}
	}
}
