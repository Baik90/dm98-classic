using SandboxEditor;
[GameResource( "Weapon", "dmcgun", "", Icon = "🗡", IconBgColor = "#5C2E33", IconFgColor = "#0e0e0e" )]
public class WeaponResource : GameResource
{

	public string Title { get; set; }
	public string Descripton { get; set; }

	[ResourceType( "vmdl" )]
	public string WorldModel { get; set; }
	public float PrimaryRate { get; set; }
	public string AmmoType { get; set; }
	public int ClipSize { get; set; }
	public float ReloadTime { get; set; }
	public int Bucket { get; set; }
	public int BucketWeight { get; set; }
}
