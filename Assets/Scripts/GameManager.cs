using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Ghost> ghosts;
    [SerializeField] public Pacman pacman;
    [SerializeField] public Ghost blinky;
    [SerializeField] public Ghost clyde;
    [SerializeField] public Ghost pinky;
    [SerializeField] public Ghost inky;
    public Transform pellets;
    public Transform powerPellets;
    [SerializeField] public Transform pacmanRespawnPoint;
    [SerializeField] public Transform blinkyRespawnPoint;
    [SerializeField] public Transform clydeRespawnPoint;
    [SerializeField] public Transform pinkyRespawnPoint;
    [SerializeField] public Transform inkyRespawnPoint;
    public int lives { get; set; }
    public int score { get; set; }
    public int combo { get; set; } = 1;

    public void Start()
    {
        NewGame();
    }
    private void NewGame() {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void Update()
    {
    
    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ghosts = FindObjectsOfType<Ghost>().ToList();
        ResetState();
        
    }

    private void ResetState()
    {
        RespawnGhost();
        this.pacman.gameObject.SetActive(true);
        RespawnPacman();

        
        
   }
    public void RespawnGhost()
    {
        Vector3 positionBlinky = this.blinky.transform.position;
        this.blinky.gameObject.SetActive(true);
        positionBlinky.x = this.blinkyRespawnPoint.position.x;
        positionBlinky.y = this.blinkyRespawnPoint.position.y;
        this.blinky.transform.position = positionBlinky;
        
        
        Vector3 positionClyde = this.clyde.transform.position;
        this.clyde.gameObject.SetActive(true);
        positionClyde.x = this.clydeRespawnPoint.position.x;
        positionClyde.y = this.clydeRespawnPoint.position.y;
        this.clyde.transform.position = positionClyde;

        Vector3 positionPinky = this.pinky.transform.position;
        this.pinky.gameObject.SetActive(true);
        positionPinky.x = this.pinkyRespawnPoint.position.x;
        positionPinky.y = this.pinkyRespawnPoint.position.y;
        this.pinky.transform.position = positionPinky;
        
        Vector3 positionInky = this.inky.transform.position;
        this.inky.gameObject.SetActive(true);
        positionInky.x = this.inkyRespawnPoint.position.x;
        positionInky.y = this.inkyRespawnPoint.position.y;
        this.inky.transform.position = positionInky;
        
        
    }
    public void RespawnPacman()
    {
        Vector3 position = this.pacman.transform.position;
        position.x = this.pacmanRespawnPoint.position.x;
        position.y = this.pacmanRespawnPoint.position.y;
        this.pacman.transform.position = position;
        this.pacman.movement.ResetState();
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
    
    private void SetScore(int score)
    {
        this.score = score;
    }
    public void AddScore(int points)
    {
        this.score += points;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten()
    {
        AddScore(Ghost.pointValue * this.combo);
    }

    public void PelletEaten()
	{
        this.AddScore(Pellet.pointValue);
        AllPelletsEaten();
	}

    public void PowerPelletEaten()
    {
        foreach (Ghost ghost in ghosts)
		{
            ghost.state = Ghost.GhostState.Afraid;
		}
        this.AddScore(PowerPellet.pointValue);
        AllPelletsEaten();
    }

    public void LoadNextLevel(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void AllPelletsEaten(){
        if(this.pellets.childCount == 0 && this.powerPellets.childCount==0){
           LoadNextLevel();
        }
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            //RespawnGhost();
            Invoke(nameof(ResetState), 3.0f);
            
        }
        else
        {
            GameOver();
        }
    }
   
}
