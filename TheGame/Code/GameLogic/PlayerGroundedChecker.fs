namespace Main

open UnityEngine

open Main

type PlayerGroundedChecker() =
    inherit MonoBehaviour() 

    let mutable player = null

    member x.Start () = 
        player <- x.GetComponentInParent<PlayerController>()

    member x.OnTriggerEnter2D (collider : Collider2D) = 
        player.SetGrounded true

    member x.OnTriggerExit2D (collider : Collider2D) = 
        player.SetGrounded false