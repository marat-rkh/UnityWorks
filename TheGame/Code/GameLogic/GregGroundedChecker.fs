namespace Main

open UnityEngine

open Main

type GregGroundedChecker() =
    inherit MonoBehaviour() 

    let mutable gregController = null

    member x.Start () = 
        gregController <- x.GetComponentInParent<GregController>()

    member x.OnTriggerEnter2D (collider : Collider2D) = 
        gregController.SetGrounded true

    member x.OnTriggerExit2D (collider : Collider2D) = 
        gregController.SetGrounded false