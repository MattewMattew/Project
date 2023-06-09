using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    

  private void Awake(){ 
    GetComponent<Animation>().Play("Bang");
  
  }
    
   
    

    
}
