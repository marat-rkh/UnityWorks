namespace Main

open UnityEngine

[<AllowNullLiteral>]
type PlayerController() =
    inherit MonoBehaviour()

    [<SerializeField>]
    let mutable speed = 30.0f

    let mutable rb2d = null
    let mutable animator = null
    let mutable grounded = true

    let animatorParamSpeed = "Speed"
    let animatorParamThrowsCandies = "ThrowsCandies"
    let animatorParamGrounded = "Grounded"

    let makeXScaleNegative (s : Vector3) =
        Vector3 (-1.0f * (Mathf.Abs s.x), s.y, s.z)

    let makeXScalePositive (s : Vector3) =
        Vector3 (Mathf.Abs s.x, s.y, s.z)

    member x.Start () = 
        rb2d <- x.GetComponent<Rigidbody2D>()
        animator <- x.GetComponent<Animator>()

    member x.Update () =
        ()

    member x.FixedUpdate () =
        let h = Input.GetAxis("Horizontal")
        rb2d.velocity <- Vector2(speed * h, rb2d.velocity.y)

        animator.SetFloat(animatorParamSpeed, Mathf.Abs(rb2d.velocity.x))
        animator.SetBool(animatorParamGrounded, grounded)

        let currentScale = x.transform.localScale
        x.transform.localScale <- 
            if h <> 0.0f then
                if h < 0.0f
                then makeXScaleNegative currentScale
                else makeXScalePositive currentScale
            else currentScale

        if Input.GetKeyDown KeyCode.K
        then animator.SetTrigger(animatorParamThrowsCandies)

    member x.SetGrounded value = 
        grounded <- value