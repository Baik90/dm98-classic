﻿using Hammer;
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


[Library( "dm_ammo9mmclip" )]
[EditorModel( "models/dm_ammo_9mmclip.vmdl" )]
[Display( Name = "Ammo - 9mm Clip" )]
partial class Ammo9mmClip : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Pistol;
	public override int AmmoAmount => 17;
	public override Model WorldModel => Model.Load( "models/dm_ammo_9mmclip.vmdl" );

}

[Library( "dm_ammo9mmbox" )]
[EditorModel( "models/dm_ammo_9mmbox.vmdl" )]
[Display( Name = "Ammo - 9mm Box" )]
partial class Ammo9mmBox : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Pistol;
	public override int AmmoAmount => 200;

	public override Model WorldModel => Model.Load( "models/dm_ammo_9mmbox.vmdl" );
}



[Library( "dm_ammobuckshot" )]
[EditorModel( "models/ammo/buckshot/dm_ammo_buckshot.vmdl" )]
[Display( Name = "Ammo - Buckshot" )]
partial class AmmoBuckshot : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Buckshot;
	public override int AmmoAmount => 20;

	public override Model WorldModel => Model.Load( "models/ammo/buckshot/dm_ammo_buckshot.vmdl" );
}

[Library( "dmc_ammobuckshotlarge" )]
[EditorModel( "models/ammo/buckshot/dm_ammo_buckshot_large.vmdl" )]
[Display( Name = "Ammo - Buckshot Large" )]
partial class AmmoBuckshotLarge : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Buckshot;
	public override int AmmoAmount => 40;

	public override Model WorldModel => Model.Load( "models/ammo/buckshot/dm_ammo_buckshot_large.vmdl" );
}

[Library( "dm_ammo357" )]
[EditorModel( "models/dm_ammo_357.vmdl" )]
[Display( Name = "Ammo - 357" )]
partial class Ammo357 : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Python;
	public override int AmmoAmount => 6;

	public override Model WorldModel => Model.Load( "models/dm_ammo_357.vmdl" );
}

[Library( "dm_ammocrossbow" )]
[EditorModel( "models/dm_ammo_crossbow.vmdl" )]
[Display( Name = "Ammo - Crossbow" )]
partial class AmmoCrossbow : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Crossbow;
	public override int AmmoAmount => 5;

	public override Model WorldModel => Model.Load( "models/dm_ammo_crossbow.vmdl" );
}

[Library( "dmc_ammorocket" )]
[EditorModel( "models/ammo/rocket/dmc_rocketammo.vmdl" )]
[Display( Name = "Ammo - Gives 5 Rockets" )]
partial class AmmoRocket : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Rocket;
	public override int AmmoAmount => 5;

	public override Model WorldModel => Model.Load( "models/ammo/rocket/dmc_rocketammo.vmdl" );
}

[Library( "dmc_ammorocketlarge" )]
[EditorModel( "models/ammo/rocket/dmc_rocketammo_large.vmdl")]
[Display( Name = "Ammo - Gives 10 Rockets" )]
partial class AmmoRocketLarge : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Rocket;
	public override int AmmoAmount => 10;

	public override Model WorldModel => Model.Load( "models/ammo/rocket/dmc_rocketammo_large.vmdl" );
}

[Library( "dmc_nails" )]
[EditorModel( "models/ammo/dm_placeholderammo.vmdl" )]
[Display( Name = "Ammo - Gives 10 Rockets" )]
partial class AmmoNailgun : BaseAmmo
{
	public override AmmoType AmmoType => AmmoType.Nail;
	public override int AmmoAmount => 25;

	public override Model WorldModel => Model.Load( "models/ammo/dm_placeholderammo.vmdl" );
}

