using Godot;

public class Player : KinematicBody2D
{
    Vector2 velocity;
    const int ACCELERATION = 400;
    const int MAX_SPEED = 80;
    const int FRICTION = 500;

    AnimationTree animationTree;
    AnimationNodeStateMachinePlayback animationState;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity = Vector2.Zero;
        animationTree = GetNode<AnimationTree>("AnimationTree");
        animationTree.Active = true;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVector = Vector2.Zero;
        animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();

        if (inputVector != Vector2.Zero)
        {
            // Set animation tree blend position values based on inputVector value
            animationTree.Set("parameters/Idle/blend_position", inputVector);
            animationTree.Set("parameters/Run/blend_position", inputVector);

            animationState.Travel("Run");
            velocity = velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
        }
        else
        {
            animationState.Travel("Idle");
            // Move
            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }

        velocity = MoveAndSlide(velocity);
    }
}
