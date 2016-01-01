namespace Main

open UnityEngine

[<AllowNullLiteral>]
type GregController() =
    inherit MonoBehaviour()

    // general parameters
    [<SerializeField>]
    let mutable speed = 30.0f

    [<SerializeField>]
    let mutable jumpPower = 150.0f

    // components
    let mutable rb2d = null
    let mutable animator = null
    let mutable grounded = true

    // animator parameters
    let animatorParamSpeed = "Speed"
    let animatorParamThrowsCandies = "ThrowsCandies"
    let animatorParamGrounded = "Grounded"

    // controls' labels
    let action1Button = "Action1"
    let jumpButton = "Jump"

    let makeXScaleNegative (s : Vector3) =
        Vector3 (-1.0f * (Mathf.Abs s.x), s.y, s.z)

    let makeXScalePositive (s : Vector3) =
        Vector3 (Mathf.Abs s.x, s.y, s.z)

    let handleCharacterRotation (x : MonoBehaviour) horizontalAxis = 
        let currentScale = x.transform.localScale
        x.transform.localScale <- 
            if horizontalAxis <> 0.0f then
                if horizontalAxis < 0.0f
                then makeXScaleNegative currentScale
                else makeXScalePositive currentScale
            else currentScale

    member x.Start () = 
        rb2d <- x.GetComponent<Rigidbody2D>()
        animator <- x.GetComponent<Animator>()

    member x.Update () =
        ()

    member x.FixedUpdate () =
        let h = Input.GetAxis("Horizontal")
        if grounded
        then rb2d.velocity <- Vector2(speed * h, rb2d.velocity.y)

        animator.SetFloat(animatorParamSpeed, Mathf.Abs(rb2d.velocity.x))
        animator.SetBool(animatorParamGrounded, grounded)

        handleCharacterRotation x h

        if Input.GetButtonDown action1Button
        then animator.SetTrigger(animatorParamThrowsCandies)

        if Input.GetButtonDown jumpButton && grounded
        then rb2d.AddForce (Vector2.up * jumpPower)

    member x.SetGrounded value = 
        grounded <- value