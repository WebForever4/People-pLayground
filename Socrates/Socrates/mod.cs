using UnityEngine;

namespace Mod
{
    public class Mod
    {
        public static void Main()
        {
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"),
                    NameOverride = "Socrates",
                    DescriptionOverride = "If wisdom is your power, what are you without it?",
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    ThumbnailOverride = ModAPI.LoadSprite("Thumbnails/Thumb.png"),
                    AfterSpawn = (Instance) =>
                    {
                        var skin = ModAPI.LoadTexture("Sprites/Skin.png");
                        var flesh = ModAPI.LoadTexture("Sprites/Flesh.png");
                        var bone = ModAPI.LoadTexture("Sprites/Bone.png");

                        var person = Instance.GetComponent<PersonBehaviour>();
                        person.SetBodyTextures(skin, flesh, bone, 1);

                        person.SetBruiseColor(200, 195, 180);
                        person.SetSecondBruiseColor(180, 170, 150);
                        person.SetThirdBruiseColor(160, 155, 130);
                        person.SetRottenColour(190, 185, 160);
                        person.SetBloodColour(120, 20, 20);

                        ApplySprite(Instance, "Head", "Sprites/Head.png");
                        ApplySprite(Instance, "Body/UpperBody", "Sprites/UpperBody.png");
                        ApplySprite(Instance, "Body/MiddleBody", "Sprites/MiddleBody.png");
                        ApplySprite(Instance, "Body/LowerBody", "Sprites/LowerBody.png");
                        ApplySprite(Instance, "FrontArm/UpperArmFront", "Sprites/UpperArm.png");
                        ApplySprite(Instance, "FrontArm/LowerArmFront", "Sprites/LowerArm.png");
                        ApplySprite(Instance, "BackArm/UpperArm", "Sprites/UpperArm.png");
                        ApplySprite(Instance, "BackArm/LowerArm", "Sprites/LowerArm.png");
                        ApplySprite(Instance, "FrontLeg/UpperLegFront", "Sprites/UpperLeg.png");
                        ApplySprite(Instance, "FrontLeg/LowerLegFront", "Sprites/LowerLeg.png");
                        ApplySprite(Instance, "BackLeg/UpperLeg", "Sprites/UpperLeg.png");
                        ApplySprite(Instance, "BackLeg/LowerLeg", "Sprites/LowerLeg.png");
                        ApplySprite(Instance, "BackLeg/Foot", "Sprites/Foot.png");
                        ApplySprite(Instance, "FrontLeg/FootFront", "Sprites/Foot.png");

                        var head = Instance.transform.Find("Head");
                        if (head != null)
                        {
                            var clicker = head.gameObject.AddComponent<SocratesClicker>();
                        }
                    }
                }
            );
        }

        static void ApplySprite(GameObject instance, string path, string spritePath)
        {
            var t = instance.transform.Find(path);
            if (t == null) return;
            var sr = t.GetComponent<SpriteRenderer>();
            if (sr == null) return;
            sr.sprite = ModAPI.LoadSprite(spritePath);
        }
    }

    public class SocratesClicker : MonoBehaviour
    {
        private bool _cooldown = false;

        void OnMouseDown()
        {
            if (_cooldown) return;

            var audio = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            var clip = ModAPI.LoadSound("Sounds/quote.ogg");
            if (clip != null)
            {
                audio.PlayOneShot(clip);
            }

            _cooldown = true;
            Invoke("ResetCooldown", 1.5f);
        }

        void ResetCooldown()
        {
            _cooldown = false;
        }
    }
}