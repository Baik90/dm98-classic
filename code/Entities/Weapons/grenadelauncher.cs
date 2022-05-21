using System.ComponentModel;
using SandboxEditor;
/// <summary>
/// It Clues in the name
/// </summary>
[Library( "dmc_grenadelauncher", Title = "Grenadelauncher" )]
[EditorModel( "models/weapons/grenadelauncher/w_grenadelauncher.vmdl" )]
[Title("Grenadelauncher"), Category( "Weapon" ), Icon( "colorize" )]
[HammerEntity]
partial class Grenadelauncher : DeathmatchWeapon
{
	public static readonly Model WorldModel = Model.Load( "models/weapons/grenadelauncher/w_grenadelauncher.vmdl" );
	public override string ViewModelPath => "models/weapons/grenadelauncher/v_grenadelauncher.vmdl";

	public override float PrimaryRate => 1.5f;
	public override int Bucket => 5;
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
		{

			if ( AvailableAmmo() > 0 )
			{
				Reload();
			}
			return;
		}

		ShootEffects();
		PlaySound( "dm.grenade_throw" );

		// TODO - if zoomed in then instant hit, no travel, 120 damage


		if ( IsServer )
			using ( Prediction.Off() )
			{
				var grenade = new Grenade
				{
					Position = Owner.EyePosition + Owner.EyeRotation.Forward * 30.0f,
					Owner = Owner,
					Rotation = Owner.EyeRotation
				};

				grenade.PhysicsBody.Velocity = Owner.EyeRotation.Forward * 600.0f + Owner.EyeRotation.Up * 200.0f + Owner.Velocity;
				//need fix for getting hit by own grenade
				//grenade.SetInteractsExclude( CollisionLayer.Player );
				_ = grenade.BlowIn( 3.0f );
			}
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		Zoomed = Input.Down( InputButton.PrimaryAttack );
	}



	public override void BuildInput( InputBuilder owner )
	{
		if ( Zoomed )
		{
			owner.ViewAngles = Angles.Lerp( owner.OriginalViewAngles, owner.ViewAngles, 0.2f );
		}
	}
	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetAnimParameter( "holdtype", 3 ); // TODO this is shit
		anim.SetAnimParameter( "aim_body_weight", 1.0f );
	}
	[ClientRpc]

	protected override void ShootEffects() 
	{

		Host.AssertClient();



		ViewModelEntity?.SetAnimParameter( "fire", true );
	}
}


