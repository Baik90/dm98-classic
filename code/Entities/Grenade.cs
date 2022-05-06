﻿[Library( "dmc_grenade", Title = "Grenade" )]
partial class Grenade : ModelEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/grenade_projectile/projectile_grenade.vmdl" );

	Particles GrenadeParticles;

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

		GrenadeParticles = Particles.Create( "particles/grenade_trail.vpcf", this, "trail_particle", true );
		GrenadeParticles.SetPosition( 0, Position );
	}

	public async Task BlowIn( float seconds )
	{
		await Task.DelaySeconds( seconds );

		DeathmatchGame.Explosion( this, Owner, Position, 400, 100, 1.0f );
		Delete();
	}
}
