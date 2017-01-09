using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class Parent {
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id {
			get;
			set;
		}
		[DataMember(Name = "displayId", EmitDefaultValue = false)]
		public string DisplayId {
			get;
			set;
		}
	}
}