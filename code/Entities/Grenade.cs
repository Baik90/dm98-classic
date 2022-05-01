partial class Grenade : BasePhysics
{
	public static readonly Model WorldModel = Model.Load( "models/items/grenade_projectile/projectile_grenade.vmdl" );

	public override void Spawn()
	{
		base.Spawn();

		Model = WorldModel;


	}

	[Event.Tick.Server]
	public virtual void Simulate()
	{
		if ( !IsServer )
			return;

		var velocity = Rotation.Forward;

		var start = Position;
		var end = start + velocity * Time.Delta;

		var trace = Trace.Ray( Position, Position )
			.HitLayer( CollisionLayer.Water, true )
			.Size( 24 )
			.Ignore( this )
			.Ignore( Owner )
			.Run();

		Position = trace.EndPosition;

		if ( trace.Hit == true )
		{
			BlowUp();
		}
	}

	public void BlowUp()
	{
		DeathmatchGame.Explosion( this, Owner, Position, 250, 100, 1.0f );
		Delete();
	}
}
