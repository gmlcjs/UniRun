using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;             //�׾��� �� ����� �����Ŭ��
    public float jumpForce = 700f;          //���� �ϴ� ��

    int jumpCount = 0;                      //���� ������ Ȱ���� ����
    bool isGrounded = false;                //�������� �ƴ��� Ȯ���ϴ� ����
    public bool isDead = false;                    //�÷��̾��� ������� ����

    Rigidbody2D rb;                         //�÷��̾��� �����ٵ�
    Animator anim;                          //�÷��̾��� �ִϸ�����
    AudioSource audioSource;                      //�÷��̾� ������Ʈ�� ������ҽ� ������Ʈ
    private bool isTouchEnded = false; // 터치 여부

    // Start is called before the first frame update
    void Start()
    {
        // 게임오브젝트로부터 사용할 컴포넌트들을 가져와 변수에할당
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        //게임 시작 전일 때 동작하지 않음
        if (!GameManager.isGameStart) return;
         // 클릭 or 최대점프 횟수 2에 도달하지 않았을 때
        if (jumpCount < 2)
        {
            // 마우스 클릭 또는 모바일 터치 처리
            if (Input.GetMouseButtonDown(0) || 
                (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isTouchEnded))
            {
                jumpCount++;
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, jumpForce));
                audioSource.Play();
                
                // 터치 상태 트래킹
                isTouchEnded = true;
            }
        }

        // 터치 종료 감지
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isTouchEnded = false;
        }

        // 마우스 버튼 떼기 처리 (PC용)
        if (Input.GetMouseButtonUp(0) && rb.velocity.y > 0)
        {
            rb.velocity = rb.velocity * 0.5f;
        }

         // 애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        anim.SetBool("Grounded", isGrounded);

          // 현재 플레이어의 x 좌표 확인
        if (transform.position.x != -7f)
        {
            Vector3 targetPosition = new Vector3(-7f, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 1 * Time.deltaTime);
        }
    }

    void Die()
    {
        //��� ó��
        //�ִϸ������� DieƮ���Ÿ� �۵�
        anim.SetTrigger("Die");
        //����� �ҽ��� ����� Ŭ���� deathClip���� ����
        audioSource.clip = deathClip;
        //��� ȿ������ ���
        audioSource.Play();

         //속도를 0,0 으로 변경
        rb.velocity = Vector2.zero;
         // 사망 상태를 true로 변경
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹�� �����ϴ� ����
        //�浹�� ������ �±װ� Dead�̰� isDead�� false�� ���
        if (other.tag == "Dead" && !isDead)
        {
            //Die�޼��带 �����ؼ� �÷��̾ ������·� ����
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���鿡 ������ ��� �����ϴ� ����
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //���鿡�� ����� ��� �����ϴ� ����
        isGrounded = false;
    }
}
