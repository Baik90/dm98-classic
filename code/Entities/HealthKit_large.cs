using System.ComponentModel;
using SandboxEditor;
/// <summary>
/// Gives 25 Healthpoints
/// </summary>
[Library( "dmc_healthkit_large", Title = "HealthKit Large" )]
[EditorModel( "models/items/healthkit/healthkit_m.vmdl" )]
[Title("Large Healthkit"), Category( "Health" ), Icon( "heart_broken" )]
[HammerEntity]
partial class HealthKitLarge : ModelEntity, IRespawnableEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/healthkit/healthkit_m.vmdl" );

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;
		Tags.Add( "trigger" );
		PhysicsEnabled = false;
		UsePhysicsCollision = false;

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
