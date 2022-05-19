using Hammer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

partial class BaseAmmo : ModelEntity, IRespawnableEntity
{
	//public static Model WorldModel = Model.Load( "models/dm_battery.vmdl" );

	public virtual AmmoType AmmoType => AmmoType.None;
	public virtual int AmmoAmount => 17;
	public virtual Model WorldModel => Model.Load( "models/dm_battery.vmdl" );

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;

		PhysicsEnabled = true;
		UsePhysicsCollision = true;

		CollisionGroup = CollisionGroup.Weapon;
		SetInteractsAs( CollisionLayer.Debris );
	}

	public override void Touch( Entity other )
	{
		base.Touch( other );

		if ( other is not DeathmatchPlayer player )
			return;

		if ( other.LifeState != LifeState.Alive )
			return;

		var ammoTaken = player.GiveAmmo( AmmoType, AmmoAmount );

		if ( ammoTaken == 0 )
			return;

		Sound.FromWorld( "dm.pickup_ammo", Position );
		PickupFeed.OnPickup( To.Single( player ), $"+{ammoTaken} {AmmoType}" );

		ItemRespawn.Taken( this );
		Delete();
	}
}


/// <summary>
/// Remove me!
/// </summary>
[Library( "dm_ammo9mmclip" )]
[EditorModel( "models/dm_ammo_9mmclip.vmdl" )]
[Display( Name = "Ammo - 17 9mm" )]
partial class Ammo9mmClip : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Pistol;
	public override int AmmoAmount => 17;
	public override Model WorldModel => Model.Load( "models/dm_ammo_9mmclip.vmdl" );

}


/// <summary>
/// Remove me!
/// </summary>
[Library( "dm_ammo9mmbox" )]
[EditorModel( "models/dm_ammo_9mmbox.vmdl" )]
[Display( Name = "Ammo - 9mm Box" )]
partial class Ammo9mmBox : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Pistol;
	public override int AmmoAmount => 200;

	public override Model WorldModel => Model.Load( "models/dm_ammo_9mmbox.vmdl" );
}


/// <summary>
/// Gives 20 Buckshot Ammo
/// </summary>
[Library( "dm_ammobuckshot" )]
[EditorModel( "models/ammo/buckshot/dm_ammo_buckshot.vmdl" )]
[Display( Name = "Ammo - Buckshot"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoBuckshot : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Buckshot;
	public override int AmmoAmount => 20;

	public override Model WorldModel => Model.Load( "models/ammo/buckshot/dm_ammo_buckshot.vmdl" );
}


/// <summary>
/// Gives 40 Buckshot Ammo
/// </summary>
[Library( "dmc_ammobuckshotlarge" )]
[EditorModel( "models/ammo/buckshot/dm_ammo_buckshot_large.vmdl" )]
[Display( Name = "Ammo - Buckshot large"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoBuckshotLarge : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Buckshot;
	public override int AmmoAmount => 40;

	public override Model WorldModel => Model.Load( "models/ammo/buckshot/dm_ammo_buckshot_large.vmdl" );
}


/// <summary>
/// Gives 5 Rockets
/// </summary>
[Library( "dmc_ammorocket" )]
[EditorModel( "models/ammo/rocket/dmc_rocketammo.vmdl" )]
[Display( Name = "Ammo - Rockets"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoRocket : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Rocket;
	public override int AmmoAmount => 5;

	public override Model WorldModel => Model.Load( "models/ammo/rocket/dmc_rocketammo.vmdl" );
}


/// <summary>
/// Gives 10 Rockets
/// </summary>
[Library( "dmc_ammorocketlarge" )]
[EditorModel( "models/ammo/rocket/dmc_rocketammo_large.vmdl")]
[Display( Name = "Ammo - Rockets Large"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoRocketLarge : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Rocket;
	public override int AmmoAmount => 10;

	public override Model WorldModel => Model.Load( "models/ammo/rocket/dmc_rocketammo_large.vmdl" );
}


/// <summary>
/// Gives 25 Nails
/// </summary>
[Library( "dmc_ammonails" )]
[EditorModel( "models/ammo/dm_placeholderammo.vmdl" )]
[Display( Name = "Ammo - Nails"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoNailgun : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Nail;
	public override int AmmoAmount => 25;

	public override Model WorldModel => Model.Load( "models/ammo/dm_placeholderammo.vmdl" );
}


/// <summary>
/// Gives 50 Nails
/// </summary>
[Library( "dmc_ammonailslarge" )]
[EditorModel( "models/ammo/dm_placeholderammo.vmdl" )]
[Display( Name = "Ammo - Nails Large"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoNailgunLarge : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Nail;
	public override int AmmoAmount => 50;

	public override Model WorldModel => Model.Load( "models/ammo/dm_placeholderammo.vmdl" );
}


/// <summary>
/// Gives 6 Batterycells
/// </summary>
[Library( "dmc_ammobattery" )]
[EditorModel( "models/items/battery/dmc_battery.vmdl" )]
[Display( Name = "Ammo - Batterycell"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoBattery : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Cell;
	public override int AmmoAmount => 6;

	public override Model WorldModel => Model.Load( "models/items/battery/dmc_battery.vmdl" );
}


/// <summary>
/// Gives 12 Batterycells
/// </summary>
[Library( "dmc_ammobatterylarge" )]
[EditorModel( "models/items/battery/dmc_batterylarge.vmdl" )]
[Display( Name = "Ammo - Batterycell Large"), Category( "Ammo" ), Icon( "hdr_auto" )]
partial class AmmoBatteryLarge : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Cell;
	public override int AmmoAmount => 12;

	public override Model WorldModel => Model.Load( "models/items/battery/dmc_batterylarge.vmdl" );
}
