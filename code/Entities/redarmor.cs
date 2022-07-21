using SandboxEditor;
/// <summary>
/// Gives 200 Armor
/// </summary>
[Library( "dmc_redarmor", Title = "Red Armor" )]
[EditorModel( "models/items/armor/armor_red.vmdl" )]
[Title("Red Armor" ), Category( "Armor" ), Icon( "security" )]
[HammerEntity]
partial class ArmorRed : ModelEntity, IRespawnableEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/armor/armor_red.vmdl" );

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;
		Tags.Add( "pickup" );
		PhysicsEnabled = false;
		UsePhysicsCollision = false;
	}

	public override void StartTouch( Entity other )
	{
		base.StartTouch( other );

		if ( other is not DeathmatchPlayer player ) return;
		if ( player.Armour >= 200 ) return;

		var newhealth = player.Armour + 200;

		newhealth = newhealth.Clamp( 0, 200 );

		player.Armour = newhealth;

		Sound.FromWorld( "dmc.pickup_armor", Position );
		PickupFeed.OnPickup( To.Single( player ), $"Red Armor" );

		ItemRespawn.Taken( this );
		Delete();
	}
}
