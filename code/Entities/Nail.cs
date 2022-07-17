[Library( "Nails" )]
[HideInEditor]
partial class Nail : ModelEntity
{
	public static readonly Model WorldModel = Model.Load( "models/items/projectile_nail/projectile_nail.vmdl" );

	bool Stuck;

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

		if ( Stuck )
			return;

		float Speed = 1000.0f;
		var velocity = Rotation.Forward * Speed;

		var start = Position;
		var end = start + velocity * Time.Delta;

		var tr = Trace.Ray( start, end )
				.UseHitboxes()
				.Ignore( Owner )
				.Ignore( this )
				.Size( 1.0f )
				.Run();


		if ( tr.Hit )
		{
			// TODO: CLINK NOISE (unless flesh)

			// TODO: SPARKY PARTICLES (unless flesh)

			Stuck = true;
			Position = tr.EndPosition + Rotation.Forward * -1;

			if ( tr.Entity.IsValid() )
			{
				var damageInfo = DamageInfo.FromBullet( tr.EndPosition, tr.Direction * 200, 10 )
													.UsingTraceResult( tr )
													.WithAttacker( Owner )
													.WithWeapon( this );

				tr.Entity.TakeDamage( damageInfo );
			}

			// TODO: Parent to bone so this will stick in the meaty heads
			SetParent( tr.Entity, tr.Bone );
			Owner = null;

			//
			// Surface impact effect
			//
			tr.Normal = Rotation.Forward * -1;
			tr.Surface.DoBulletImpact( tr );
			velocity = default;

			// delete self in 60 seconds
			_ = DeleteAsync( 60.0f );
		}
		else
		{
			Position = end;
		}
	}
}
