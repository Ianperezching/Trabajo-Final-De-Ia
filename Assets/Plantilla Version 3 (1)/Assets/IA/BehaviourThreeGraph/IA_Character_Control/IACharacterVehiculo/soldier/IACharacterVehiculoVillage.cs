using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACharacterVehiculoVillage : IACharacterVehiculo
{
    public float safeDistance = 10f;
    public FuzzySpeed fuzzySpeed = new FuzzySpeed();
    public CalculateDiffuse diffuseCalculator; 

    private Animator animator;
    private float currentDangerLevel = 0f;

    public override void LoadComponent()
    {
        base.LoadComponent();
        health.typeAgent = TypeAgent.A;
        health.typeAgentAllies.Add(TypeAgent.A);

   
        if (fuzzySpeed.speedCurve == null || fuzzySpeed.speedCurve.length == 0)
        {
            fuzzySpeed.speedCurve = new AnimationCurve(
                new Keyframe(0f, 0f),
                new Keyframe(0.5f, 0.2f),
                new Keyframe(1f, 1f)
            );
        }

        animator = GetComponent<Animator>();
        diffuseCalculator = GetComponent<CalculateDiffuse>();
    }

    private void Update()
    {
        CalculateDangerLevel();
        fuzzySpeed.Evaluate(currentDangerLevel);

        
        if (AIEye.ViewEnemy != null)
        {
            FleeFromZombie();
        }
        else
        {
            MoveToWander();
        }

        LookRotationCollider();
        UpdateAnimator();
    }

    private void CalculateDangerLevel()
    {
        if (AIEye.ViewEnemy == null)
        {
            currentDangerLevel = 0f;
            return;
        }

        float distanceToZombie = AIEye.DistanceEnemy;
        float normalizedDistance = Mathf.Clamp01(1 - (distanceToZombie / safeDistance));


        if (diffuseCalculator != null)
        {
         
            currentDangerLevel = Mathf.Max(normalizedDistance, diffuseCalculator.speedRotation * 0.1f);
        }
        else
        {
            currentDangerLevel = normalizedDistance;
        }
    }

    private void FleeFromZombie()
    {
        if (AIEye.ViewEnemy == null) return;

        Vector3 fleeDirection = (transform.position - AIEye.ViewEnemy.transform.position).normalized;
        Vector3 fleePosition = transform.position + fleeDirection * safeDistance;

        // Usar la velocidad calculada por la lógica difusa (aunque no afecte el movimiento)
        agent.speed = fuzzySpeed.currentSpeed;
        MoveToPosition(fleePosition);
        LookPosition(fleePosition);
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            // Actualizar parámetros del animator basados en la lógica difusa
            animator.SetFloat("Speed", fuzzySpeed.currentSpeed);
            animator.SetFloat("DangerLevel", currentDangerLevel);
        }
    }

}
    [System.Serializable]
    public class FuzzySpeed
    {
        public AnimationCurve speedCurve;
        public float minSpeed = 1f;
        public float maxSpeed = 3f;
        public float currentSpeed = 0f;

        public void Evaluate(float dangerLevel)
        {
            
            float curveValue = speedCurve.Evaluate(Mathf.Clamp01(dangerLevel));
            currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, curveValue);
        }
    }

