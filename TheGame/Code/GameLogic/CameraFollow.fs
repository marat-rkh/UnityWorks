namespace Main

open UnityEngine

type CameraFollow() = 
    inherit MonoBehaviour()

    let mutable gregController = null
    let mutable velocity = Vector2()

    [<SerializeField>]
    let mutable smoothTimeX = 0.25f
    [<SerializeField>]
    let mutable smoothTimeY = 0.25f

    member x.Start () = 
        gregController <- GameObject.FindGameObjectWithTag("Player")

    member x.FixedUpdate () =
        let posX = Mathf.SmoothDamp(x.transform.position.x, gregController.transform.position.x, &velocity.x, smoothTimeX)
        let posY = Mathf.SmoothDamp(x.transform.position.y, gregController.transform.position.y, &velocity.y, smoothTimeY)
        x.transform.position = Vector3(posX, posY, x.transform.position.z)