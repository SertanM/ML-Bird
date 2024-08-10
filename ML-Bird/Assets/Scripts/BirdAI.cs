using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System;
using Unity.MLAgents.Sensors;
using System.Collections.Generic;
using TMPro;
public class BirdAI : Agent
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;

    private int score = 0;
    private int bestScore = 0;
    public override void OnEpisodeBegin()
    {
        var pipes = FindObjectsByType<pipeMovement>(FindObjectsSortMode.None);
        foreach (var pipe in pipes){
            Destroy(pipe.gameObject);
        }
        if(score>bestScore) {
            bestScore = score;
            bestScoreText.text = "Best:" + score;
        }
        score = 0;
        scoreText.text = score.ToString();
        this.transform.position = new Vector3(-5, 0, 0);
        this.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float worldHeight = 100f;
        float birdNormalizedY = (this.transform.position.y + (worldHeight / 2f)) / worldHeight;
        sensor.AddObservation(birdNormalizedY);

        sensor.AddObservation(this.GetComponent<Rigidbody2D>().linearVelocityY);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> a = actionsOut.DiscreteActions;
        a[0] = Input.GetMouseButton(0) ? 1 : 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if(actions.DiscreteActions[0] == 1){
            Jump();
        }
    }

    private void Jump()
    {
        Rigidbody2D myRg = GetComponent<Rigidbody2D>();
        myRg.linearVelocityY = 1f;
    }

    private void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Pipe"){
            AddReward(-2f);
            EndEpisode();
        }
        else if(coll.tag == "Goal"){
            AddReward(+2f);
            score++;
            scoreText.text = score.ToString();
        }
    }
}
