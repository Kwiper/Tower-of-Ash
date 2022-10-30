using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState {


    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        //Finds if parent is a swing for the player
        player.playersSwing = player.gameObject.GetComponentInParent(typeof(Platform)) as Platform;
        if(player.playersSwing != null){
            float angularSpeedAdded = 0f;

            if(xInput != 0){
                if(xInput < 0 && player.playersSwing.getPosXDif() > 0){
                    //When traveling to the left and swing is traveling right, speed will be same as if both traveling same direction
                    angularSpeedAdded = player.playersSwing.getAngularSpeed();                    
                    Debug.Log("Player is traveling Left and Swing is traveling Right at " + (playerData.movementVelocity*xInput)*(angularSpeedAdded*player.getSwingPlatMultiplier()));
                }
                else if(xInput > 0 && player.playersSwing.getPosXDif() < 0){
                    //When traveling to the right and swing is traveling left, speed will be same as if both traveling same direction  
                    angularSpeedAdded = player.playersSwing.getAngularSpeed()*-1;
                    
                    Debug.Log("Player is traveling Right and Swing is traveling Left at " + (playerData.movementVelocity*xInput)*(angularSpeedAdded*player.getSwingPlatMultiplier()));
                }
                else if(xInput > 0 && player.playersSwing.getPosXDif() > 0){
                    //When both traveling to the right, velocity will be correct direction 
                    angularSpeedAdded = player.playersSwing.getAngularSpeed();
                    Debug.Log("Player and Swing are both traveling right at " + (playerData.movementVelocity*xInput)*(angularSpeedAdded*player.getSwingPlatMultiplier()));
                }
                else{
                    angularSpeedAdded = player.playersSwing.getAngularSpeed()*-1;
                    Debug.Log("Player and Swing are both traveling left at " + (playerData.movementVelocity*xInput)*(angularSpeedAdded*player.getSwingPlatMultiplier()));
                }
            }

            else{
                angularSpeedAdded = 0;
            }
            player.SetVelocityX((playerData.movementVelocity*xInput)*(angularSpeedAdded*player.getSwingPlatMultiplier()));
        
        }

        else{
            player.SetVelocityX((playerData.movementVelocity) * xInput);
        }
        if(xInput == 0 && !isExitingState)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
