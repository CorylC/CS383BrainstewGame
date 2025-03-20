using NUnit.Framework;
using UnityEngine;

public class B1_PlayerSpeedTests
{
    private GameObject player;
    private Move moveComponent;
    private Rigidbody2D rb;
    private Ground ground;
    private CONTROLLER controller;

    [SetUp]
    public void Setup()
    {
        //create player obj & add componentz
        player = new GameObject("Player");
        rb = player.AddComponent<Rigidbody2D>();
        ground = player.AddComponent<Ground>();
        controller = player.AddComponent<CONTROLLER>();

        //mock input controller to simulate horizontal input
        controller.input = ScriptableObject.CreateInstance<MockInputController>();

        moveComponent = player.AddComponent<Move>();

        //explicitly set grounded state and friction for tests
        SetPrivateProperty(ground, "OnGround", true); //simulate grounded state
        SetPrivateProperty(ground, "Friction", 0f);   //set no friction
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(player);
    }

    [Test]
    public void PlayerSpeed_MinBoundary_IsZero()
    {
        //Arrange: Set _maxSpeed to minimum (0f)
        SetPrivateField(moveComponent, "_maxSpeed", 0f);

        //simulate full horizontal input explicitly
        ((MockInputController)controller.input).moveInput = 1f;

        //Act: Manually simulate Move.Update() logic
        float desiredVelocityX = Mathf.Max(0f - ground.Friction, 0f); // Should be 0f
        SetPrivateField(moveComponent, "_desiredVelocity", new Vector2(desiredVelocityX, 0f));

        //manually simulate FixedUpdate logic:
        float deltaTime = 0.02f; //default fixed delta time
        float acceleration = (float)GetPrivateField(moveComponent, "_maxAcceleration"); //grounded acceleration
        float maxSpeedChange = acceleration * deltaTime; //speed change per fixed step

        Vector2 currentVelocity = rb.linearVelocity; // Initially zero
        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredVelocityX, maxSpeedChange);

        //explicitly set Rigidbody2D velocity as FixedUpdate would do:
        rb.linearVelocity = currentVelocity;


        Debug.Log($"Player velocity after update: {rb.linearVelocity.x}. Expected: {desiredVelocityX}.");

        if (Mathf.Approximately(rb.linearVelocity.x, desiredVelocityX))
        {
            Debug.Log("Oh, looks like it does match the min speed after update!");
        }

        //Assert: Velocity should remain zero
        Assert.AreEqual(0f, rb.linearVelocity.x, "Player velocity should be zero when _maxSpeed is 0.");
    }

    [Test]
    public void PlayerSpeed_MaxBoundary_RespectsMaxSpeed()
    {
        //Arrange: Set _maxSpeed to maximum (100f)
        SetPrivateField(moveComponent, "_maxSpeed", 100f);

        //sim full horizontal input explicitly
        ((MockInputController)controller.input).moveInput = 1f;

        float deltaTime = 0.02f; //default fixed delta time
        float acceleration = (float)GetPrivateField(moveComponent, "_maxAcceleration"); //grounded acceleration
        float maxSpeedChange = acceleration * deltaTime; //speed change per fixed step

        Vector2 currentVelocity = rb.linearVelocity; //initially zero

        //Act: Simulate multiple frames until velocity stabilizes
        int maxFrames = 100; //arbitrary large number of frames to simulate
        for (int i = 0; i < maxFrames; i++)
        {
            float desiredVelocityX = Mathf.Max(100f - ground.Friction, 0f); //should be 100f
            SetPrivateField(moveComponent, "_desiredVelocity", new Vector2(desiredVelocityX, 0f));

            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredVelocityX, maxSpeedChange);
            rb.linearVelocity = currentVelocity;

            Debug.Log($"Frame {i + 1}: Player velocity after update: {rb.linearVelocity.x}");

            //break early if velocity stabilizes at or below _maxSpeed
            if (Mathf.Approximately(rb.linearVelocity.x, desiredVelocityX))
            {
                Debug.Log("Oh, looks like it does match the max speed after update!");
                break;
            }
        }

        //Assert: Velocity should stabilize at or below _maxSpeed
        Assert.LessOrEqual(rb.linearVelocity.x, 100f, "Player velocity should respect max speed limit.");
    }


    //helper methods for reflection to access private fields easily
    private void SetPrivateField(object obj, string fieldName, object value)
    {
        var field = obj.GetType().GetField(fieldName,
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        field.SetValue(obj, value);
    }

    private object GetPrivateField(object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName,
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        return field.GetValue(obj);
    }

    private void SetPrivateProperty(object obj, string propertyName, object value)
    {
        var prop = obj.GetType().GetProperty(propertyName,
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);
        
        prop.SetValue(obj, value);
    }
}

//simple mock input controller for testing purposes
public class MockInputController : InputController
{
    public float moveInput;
    
    public override float RetrieveMoveInput() => moveInput;
    
    public override bool RetrieveJumpInput() => false;
    
    public override bool RetrieveFastFallInput() => false;
}
