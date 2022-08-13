using SandboxEditor;
/// <summary>
/// Shoots explosiv Rockets
/// </summary>
[Library( "dmc_rocketlauncher", Title = "Rocketlauncher" )]
[EditorModel( "models/weapons/rocketlauncher/w_rocketlauncher.vmdl" )]
[Title("Rocketlauncher"), Category( "Weapon" ), Icon( "colorize" )]
[HammerEntity]
partial class Rocketlauncher : DeathmatchWeapon
{
	public static readonly Model WorldModel = Model.Load( "models/weapons/rocketlauncher/w_rocketlauncher.vmdl" );
	public override string ViewModelPath => "models/weapons/rocketlauncher/v_rocketlauncher.vmdl";

	public override float PrimaryRate => 1;
	public override int Bucket => 6;
	public override int BucketWeight => 100;
	public override AmmoType AmmoType => AmmoType.Rocket;
	public override int ClipSize => 5;

	[Net, Predicted]
	public bool Zoomed { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		AmmoClip = 5;
		Model = WorldModel;
	}

	public override void AttackPrimary()
	{
		if ( !TakeAmmo( 1 ) )


		ShootEffects();
		PlaySound( "rust_crossbow.shoot" );

		// TODO - if zoomed in then instant hit, no travel, 120 damage


		if ( IsServer )
		{
			var bolt = new Rocket();
			bolt.Position = Owner.EyePosition + Owner.EyeRotation.Forward * 30.0f;
			bolt.Rotation = Owner.EyeRotation;
			bolt.Owner = Owner;
			bolt.Velocity = Owner.EyeRotation.Forward * 1;
		}
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		Zoomed = Input.Down( InputButton.SecondaryAttack );
	}

	public override void PostCameraSetup( ref CameraSetup camSetup )
	{
		base.PostCameraSetup( ref camSetup );

		if ( Zoomed )
		{
			camSetup.FieldOfView = 20;
			camSetup.ViewModel.FieldOfView = 40;
		}
	}

	public override void BuildInput( InputBuilder owner )
	{
		if ( Zoomed )
		{
			owner.ViewAngles = Angles.Lerp( owner.OriginalViewAngles, owner.ViewAngles, 0.2f );
		}
	}

	[ClientRpc]
	protected override void ShootEffects()
	{
		Host.AssertClient();

		ViewModelEntity?.SetAnimParameter( "fire", true );
	}
	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetAnimParameter( "holdtype", 3 ); // TODO this is shit
		anim.SetAnimParameter( "aim_body_weight", 1.0f );
	}
}
