using Godot;

public class Player : KinematicBody2D
{
    Vector2 velocity;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity = Vector2.Zero;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVector = Vector2.Zero;

        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        if (inputVector != Vector2.Zero)
        {
            velocity = inputVector;
        }
        else
        {
            velocity = Vector2.Zero;
        }

        MoveAndCollide(velocity);
    }
}
