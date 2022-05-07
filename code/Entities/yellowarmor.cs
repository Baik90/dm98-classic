using System.ComponentModel.DataAnnotations;
using Hammer;
using System.ComponentModel;

/// <summary>
/// Gives 150 Armor
/// </summary>
[Library( "dmc_yellowarmor", Title = "Yellow Armor" )]
[EditorModel( "models/items/armor/armor_yellow.vmdl" )]
[Display( Name = "Yellow Armor" ), Category( "Armor" ), Icon( "security" )]
partial class ArmorYellow : ModelEntity, IRespawnableEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/armor/armor_yellow.vmdl" );

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

		if ( other is not DeathmatchPlayer player ) return;
		if ( player.Armour >= 100 ) return;

		var newhealth = player.Armour + 150;

		newhealth = newhealth.Clamp( 0, 100 );

		player.Armour = newhealth;

		Sound.FromWorld( "dm.item_battery", Position );
		PickupFeed.OnPickup( To.Single( player ), $"+150 Armour" );

		ItemRespawn.Taken( this );
		Delete();
	}
}
