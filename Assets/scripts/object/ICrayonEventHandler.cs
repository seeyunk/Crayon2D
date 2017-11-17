using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crayon;


public interface ICrayonEventHandler : UnityEngine.EventSystems.IEventSystemHandler
{
	void fire( FireState fireState );
	void turnLeft();
	void turnRight();
	void boost( BoostState boostState );
}


