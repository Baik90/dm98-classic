﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// Gives 100 Healthpoints
/// </summary>
[Library( "dmc_healthkit_mega", Title = "HealthKit Mega" )]
[Hammer.EditorModel( "models/items/healthkit/healthkit_l.vmdl" )]
[Display( Name = "Mega Healthkit", GroupName = "Health", Description = "Gives 100 healthpoints." ), Category( "Health" ), Icon( "heart_broken" )]
partial class HealthKitMega : ModelEntity, IRespawnableEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/healthkit/healthkit_l.vmdl" );

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;

		PhysicsEnabled = true;
		UsePhysicsCollision = true;

		CollisionGroup = CollisionGroup.Weapon;
		SetInteractsAs( CollisionLayer.Debris );
	}

	public override void StartTouch( Entity other )
	{
		base.StartTouch( other );

		if ( other is not DeathmatchPlayer pl ) return;
		if ( pl.Health >= pl.MaxHealth ) return;

		var newhealth = pl.Health + 100;

		newhealth = newhealth.Clamp( 0, pl.MaxHealth );

		pl.Health = newhealth;

		Sound.FromWorld( "dm.item_health", Position );
		ItemRespawn.Taken( this );
		Delete();
	}
}
