using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Library( "dmc_grenadelauncher", Title = "Grenadelauncher" )]
[Hammer.EditorModel( "models/weapons/rocketlauncher/w_rocketlauncher.vmdl" )]
[Display( Name = "Grenadelauncher"), Category( "Weapon" ), Icon( "colorize" )]
partial class Grenadelauncher : DeathmatchWeapon
{
	public static readonly Model WorldModel = Model.Load( "models/weapons/rocketlauncher/w_rocketlauncher.vmdl" );
	public override string ViewModelPath => "models/weapons/rocketlauncher/v_rocketlauncher.vmdl";

	public override float PrimaryRate => 1;
	public override int Bucket => 5;
	public override int BucketWeight => 100;
	public override AmmoType AmmoType => AmmoType.Grenade;
	public override int ClipSize => 5;

	[Net, Predicted]
	public bool Zoomed { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		AmmoClip = 5;
		Model = WorldModel;
	}

	public override bool CanPrimaryAttack()
	{
		return Input.Released( InputButton.Attack1 );
	}

	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;
		TimeSinceSecondaryAttack = 0;

		if ( Owner is not DeathmatchPlayer player ) return;

		if ( !TakeAmmo( 1 ) )
		{
			Reload();
			return;
		}

		// woosh sound
		// screen shake

		PlaySound( "dm.grenade_throw" );

		Rand.SetSeed( Time.Tick );


		if ( IsServer )
			using ( Prediction.Off() )
			{
				var grenade = new Grenade
				{
					Position = Owner.EyePosition + Owner.EyeRotation.Forward * 3.0f,
					Owner = Owner
				};

				grenade.PhysicsBody.Velocity = Owner.EyeRotation.Forward * 600.0f + Owner.EyeRotation.Up * 200.0f + Owner.Velocity;

				// This is fucked in the head, lets sort this this year
				grenade.CollisionGroup = CollisionGroup.Debris;
				grenade.SetInteractsExclude( CollisionLayer.Player );
				grenade.SetInteractsAs( CollisionLayer.Debris );

				_ = grenade.BlowIn( 3.0f );
			}

		player.SetAnimParameter( "b_attack", true );

		Reload();

		if ( IsServer && AmmoClip == 0 && player.AmmoCount( AmmoType.Grenade ) == 0 )
		{
			Delete();
			player.SwitchToBestWeapon();
		}
	}
	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetAnimParameter( "holdtype", 3 ); // TODO this is shit
		anim.SetAnimParameter( "aim_body_weight", 1.0f );
	}
}
