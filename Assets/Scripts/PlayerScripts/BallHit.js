#pragma strict

function Start () {

}

function Update () {

}

function ApplyHit(other : GameObject)
{
	var moveDirection : Vector3 = this.transform.position - other.transform.position;
	GetComponent(Rigidbody).AddForce(moveDirection * 150);
}