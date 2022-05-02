partial class Rocket : ModelEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/rocket/projectile_rocket.vmdl" );


	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;
		 

	}


	[Event.Tick.Server]
	public virtual void Tick()
	{
		if ( !IsServer )
			return;


		float Speed = 750.0f;
		var velocity = Rotation.Forward * Speed;

		var start = Position;
		var end = start + velocity * Time.Delta;

		var tr = Trace.Ray( start, end )
				.UseHitboxes()
				//.HitLayer( CollisionLayer.Water, !InWater )
				.Ignore( Owner )
				.Ignore( this )
				.Size( 1.0f )
				.Run();


		if ( tr.Hit )
		{
			DeathmatchGame.Explosion( this, Owner, Position, 400, 150, 1.0f );
			Delete();

			if ( tr.Entity.IsValid() )
			{
				var damageInfo = DamageInfo.FromBullet( tr.EndPosition, tr.Direction * 200, 250 )
													.UsingTraceResult( tr )
													.WithAttacker( Owner )
													.WithWeapon( this );

				tr.Entity.TakeDamage( damageInfo );
			}




		}
		else
		{
			Position = end;
		}
	}
}
