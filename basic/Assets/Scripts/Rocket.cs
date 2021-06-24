using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody _rigidbody;
    AudioSource audioSource;
    float _rotationspeed;
    [SerializeField]
    float rcsThrust = 10f;
    [SerializeField]
    float mainThrust = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip sucess;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem rocketJetP;
    [SerializeField] ParticleSystem sucessP;
    [SerializeField] ParticleSystem deathP;
    // Start is called before the first frame update
    enum State { Alive,Dying,Transcending};
    State state = State.Alive; 
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Rotate();
            Thrust();
        }
    } 
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
            rocketJetP.Play();

        }
        else
        {
            audioSource.Stop();
            rocketJetP.Stop();
        }
    }
    void Rotate()
    {
        _rigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            _rotationspeed = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * _rotationspeed);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rotationspeed = rcsThrust * Time.deltaTime;
            transform.Rotate(-Vector3.forward * _rotationspeed) ;
        }
        _rigidbody.freezeRotation = false;
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
    void LoadFirst()
    {
        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter(Collision collision)
    {  if(state != State.Alive)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                
                break;
            case "Finish":
                StartSucessSequence();
                break;
            default:
                StartDeathSequence();
                break; 
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathP.Play();
        Invoke("LoadFirst", 2f);
    }
    private void StartSucessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(sucess);
        sucessP.Play();
        Invoke("LoadNextScene", 2f);
    }
}
