using Godot;
using System;

public partial class testplayerMovement : CharacterBody2D
{
    
    public const float Speed = 100.0f;


    //public Vector2 playerpos;



    //public PlayerMovement PlayerMovementInstance { get; private set; }


    //private AnimatedSprite2D _animatedSprite_2;

    //// Get the gravity from the project settings to be synced with RigidBody nodes.
    //public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        YSortEnabled = true;
        //profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");
        //PlayerMovementInstance = GetNode<PlayerMovement>("/root/Main/World/Player");
        // Получаем ссылку на AnimatedSprite2D
        //_animatedSprite_1 = GetNode<AnimatedSprite2D>("animation/AnimatedCharacter");
        //_animatedSprite_2 = GetNode<AnimatedSprite2D>("animation/AnimatedHat");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        //playerpos = velocity;

        //// Add the gravity.
        //if (!IsOnFloor())
        //	velocity.Y += gravity * (float)delta;

        //// Handle Jump.
        //if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        //	velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("left", "right", "up", "down");

        

            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * Speed;
                velocity.Y = direction.Y * Speed;
                

                //_animatedSprite_2.Animation = "walk";
                //_animatedSprite_2.FlipH = direction.X < 0;

            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
                velocity.Y = Mathf.MoveToward(Velocity.X, 0, Speed);
                
                //_animatedSprite_2.Animation = "idle";
            }

            Velocity = velocity;
            
            MoveAndSlide();
        

       
    }
}
