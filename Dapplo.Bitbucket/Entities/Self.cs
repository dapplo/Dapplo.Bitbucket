using System.Runtime.Serialization;

namespace Dapplo.Bitbucket.Entities
{
	[DataContract]
	public class Self {
		[DataMember(Name = "href", EmitDefaultValue = false)]
		public string Href {
			get;
			set;
		}
	}
}