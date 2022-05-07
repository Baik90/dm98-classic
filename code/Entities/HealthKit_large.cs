
using Hammer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[Library( "dmc_healthkit_large", Title = "HealthKit Large" )]
[EditorModel( "models/items/healthkit/healthkit_m.vmdl" )]
[Display( Name = "Large Healthkit", GroupName = "Health", Description = "Gives 25 healthpoints." ), Category( "Health" ), Icon( "heart_broken" )]
partial class HealthKitLarge : ModelEntity, IRespawnableEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/healthkit/healthkit_m.vmdl" );

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

		var newhealth = pl.Health + 25;

		newhealth = newhealth.Clamp( 0, pl.MaxHealth );

		pl.Health = newhealth;

		Sound.FromWorld( "dm.item_health", Position );
		ItemRespawn.Taken( this );
		Delete();
	}
}
