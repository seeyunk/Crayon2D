using System;

namespace Crayon
{
	public class Variables {
		public static float DEFAULT_MAX_SPEED = 250.0f;
		public static float DEFAULT_SPEED = 150.0f;
		public static float DEFAULT_BOOST_SPEED = 500.0f;
		public static float DEFAULT_BOOST_INC_VELOCITY = 15.0f;
		public static float DEFAULT_BOOST_DEC_VELOCITY = 3.0f;
		public static float DEFAULT_ROTATION_VELOCITY = 2.5f;
		public static float DEFAULT_PARACHUTE_VELOCITY = 100.0f;
	}

	public class Strings {
	}

	public enum FireState {
		ON,
		OFF
	}

	public enum MoveState {
		LEFT, RIGHT, NEUTRAL
	}

	public enum BoostState {
		ON, OFF, OVERHEAT
	}

	public enum PlayerState {
		NORMAL, PARACHUTE, DEAD
	}

	public enum Tag { 
		UI_CONTROLLER,
		UI_CONTROLLER_MOVE,
		UI_CONTROLLER_LEFT, 
		UI_CONTROLLER_RIGHT, 
		UI_CONTROLLER_FIRE,
		UI_CONTROLLER_BOOST,
		UI_CONTROLLER_BOOST_FILLER,
		UI_CANVAS,
		UI_LABEL,
		OBJECT_BULLET,
		OBJECT_PLAYER,
		OBJECT_PLAYER_PARACHUTE,
		OBJECT_ENEMY
	}

	public enum Layer {
		UI,
		UI_MAIN
	}
}

