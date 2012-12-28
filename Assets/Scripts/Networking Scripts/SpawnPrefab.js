var playerPrefab : Transform;

function OnNetworkLoadedLevel ()
{
	Network.Instantiate(playerPrefab, transform.position, transform.rotation, 0);
	
}

function OnPlayerDisconnected (player : NetworkPlayer)
{
//	DebugGUI.Print("Server destroying player");
	Network.RemoveRPCs(player, 0);
	Network.DestroyPlayerObjects(player);
}

static function Spawn(name : String)
{
	Network.Instantiate(Resources.Load(name), Vector3(0,0,0), Quaternion.identity, 0);
}


static function Spawn(name : String, transform : Transform)
{
	Network.Instantiate(Resources.Load(name), transform.position, transform.rotation, 0);
}
