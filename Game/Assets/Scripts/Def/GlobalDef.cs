using System.Collections;
using System.Collections.Generic;

public class GlobalDef
{
    //Common Defines
    public const int INVALID_VALUE = -1;

    //3D World
    public const float WORLD_GRAVITY = 9.8f;

    //Actor Defines
    public const float ACTOR_MAX_FOWARD_SPEED = 2.0f;
    public const float ACTOR_MOVE_SPEED = 6f;
    public const float ACTOR_DODGE_SPEED = 10f;
    public const float ACTOR_JUMP_SPEED = 19.8f;
    public const float ACTOR_JUMP_SPEED_ACCEL = 18f;
    public const float ACTOR_FOWARD_WALK_SPEED = 0.3f;
    public const float ACTOR_QUICK_TURN_SPEED = ACTOR_MAX_FOWARD_SPEED / 2;
}
