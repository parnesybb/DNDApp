using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Runtime.Serialization.Formatters.Binary;
using GoSteve.Screens;
using Server;

namespace GoSteve
{
    [Activity(Label = "GoSteve! Dungeons and Dragons")]
    public class NewChar3Screen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var gsMsg = new GSActivityMessage();
            gsMsg.Message = (byte[])Intent.Extras.Get(gsMsg.CharacterMessage);
            var cs = CharacterSheet.GetCharacterSheet(gsMsg.Message);

            CharacterSheet c = cs;

            string desc = "";

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.newChar3);

            TextView classDesc = FindViewById<TextView>(Resource.Id.classDesc);
            TextView errMsg = FindViewById<TextView>(Resource.Id.errMsg);
            Button barbarianBtn = FindViewById<Button>(Resource.Id.barbarianBtn);
            Button bardBtn = FindViewById<Button>(Resource.Id.bardBtn);
            Button clericBtn = FindViewById<Button>(Resource.Id.clericBtn);
            Button druidBtn = FindViewById<Button>(Resource.Id.druidBtn);
            Button fighterBtn = FindViewById<Button>(Resource.Id.fighterBtn);
            Button monkBtn = FindViewById<Button>(Resource.Id.monkBtn);
            Button paladinBtn = FindViewById<Button>(Resource.Id.paladinBtn);
            Button rangerBtn = FindViewById<Button>(Resource.Id.rangerBtn);
            Button rogueBtn = FindViewById<Button>(Resource.Id.rogueBtn);
            Button sorcerorBtn = FindViewById<Button>(Resource.Id.sorcerorBtn);
            Button warlockBtn = FindViewById<Button>(Resource.Id.warlockBtn);
            Button wizardBtn = FindViewById<Button>(Resource.Id.wizardBtn);
            Button continueBtn = FindViewById<Button>(Resource.Id.continueBtn);

            barbarianBtn.Click += (s, arg) =>
            {
                desc = "Barbarians come alive in the chaos of combat.  They can enter a berserk state where rage takes over, giving them superhuman strength and resilience.  A barbarian can draw on this reservoir of fury only a few times without resting, but those few rages are usually sufficient to defeat whatever threats arise. \nStrength and Constitution are most important to Barbarians.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.BARBARIAN, true);
            };

            bardBtn.Click += (s, arg) =>
            {
                desc = "The greatest strength of bards is their sheer versatility.  Many bards prefer to stick to the sidelines in combat, using their magic to inspire their allies and hinder their foes fram a distance, but bards are capable of defending themselves in melee if necessary, using their magic to bolster their swords and armar. Their spells lean toward charms and illusions rather than blatantly destructive spells. They have a wide-ranging knowledge of many subjects and a natural aptitude that lets them do almost anything well.Bards become masters of the talents they set their minds to perfecting, from musical performance to esoteric knowledge. \nCharisma and Dexterity are most important to Bards.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.BARD, true);
            };

            clericBtn.Click += (s, arg) =>
            {
                desc = "Clerics combine the helpful magic of healing and inspiring their allies with spells that harm and hinder foes. They can provoke awe and dread, lay curses of plague or poison, and even call down liames from heaven to consume their enemies.For those evildoers who will benefit most from a mace to the head, clerics depend on their combat training to let them wade into melee with the power of the gods on their side. \nWisdom and Constitution are most important to Clerics.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.CLERIC, true);
            };

            druidBtn.Click += (s, arg) =>
            {
                desc = "Druid spells are oriented toward nature and animalsthe power of toolh and claw, of sun and moon, of fire and storm. Druids also gain the ability to take on animal forms, and some druids make a particular study of this practice, even to the point where they prefer animal form to their natural form. \nWisdom and Constitution are most important to Druids.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.DRUID, true);
            };

            fighterBtn.Click += (s, arg) =>
            {
                desc = "Fighters learn the basics of ali combat styles. Every fighter can swing an axe, fence wilh a rapier, wield a longsword or a grealsword, use a bow, and even trap foes in a net with some degree of skill.Likewise, a fighler is adept with shields and every form of armor.Beyond that basic degree of familiarity, each fighler specializes in a cerlain style of combat.Some concentrate on archery, some on fighting with two weapons aI once, and some on augmenting their martial skills with magic. \nStrength and Constitution are most important to Fighters.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.FIGHTER, true);
            };

            monkBtn.Click += (s, arg) =>
            {
                desc = "Monks make careful study of a magical energy that most monastic traditions call ki. This energy is an element of the magic that suffuses the multiverse-specifically, the element that flows through living bodies.  Monks harness this power within themselves to create magical effects and exceed their body's physical capabililies, and some of their special attacks can hinder the flow of ki in their opponents. Using this energy, monks channel uncanny speed and strength into their unarmed strikes. \nDexterity and Wisdom are most important to Monks.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.MONK, true);
            };

            paladinBtn.Click += (s, arg) =>
            {
                desc = "Paladins train for years to learn the skills of combat, mastering a variety of weapons and armor.  Even so, their martial skills are secondary to the magical power they wield: power to heal the sick and injured, to smite the wicked and the undead, and to protect the innocent and those who join them in the fight for justice. \nStrength and Charisma are most important to Paladins.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.PALADIN, true);
            };

            rangerBtn.Click += (s, arg) =>
            {
                desc = "Thanks to their familiarity with the wilds, rangers acquire the ability to cast spells that harness nature's power, much as a druid does.  Their spells, like their combat abilities, emphasize speed, stealth, and the hunt.  A ranger's talents and abilities are honed with deadly focus on the grim task of protecting the borderlands. \nDexterity and Wisdom are most important to Rangers.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.RANGER, true);
            };

            rogueBtn.Click += (s, arg) =>
            {
                desc = "Rogues devote as much effort to mastering the use of a variety of skills as they do to perfecting their combat abilities, giving them a broad expertise that few other characters can match.  Many rogues focus on stealth and deception, while others refine the skills that help them in a dungeon environment, such as climbing, finding and disarming traps, and opening locks. \nDexterity and Charisma are most important to Rogues.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.ROGUE, true);
            };

            sorcerorBtn.Click += (s, arg) =>
            {
                desc = "Magic is a part of every sorcerer. suffusing body, mind, and spirit with a latent power that waits to be tapped.  Some sorcerers wield magic that springs from an ancient bloodline infused with the magic of dragons.  Others carry a raw, uncontrolled magic within them, a chaotic storm that manifests in unexpected ways. \nCharisma and Constitution are most important to Sorcerors.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.SORCERER, true);
            };

            warlockBtn.Click += (s, arg) =>
            {
                desc = "A warlock is defined by a pact with an otherworldly being.  Sometimes the relationship between warlock and patron is like that of a cleric and a deity, though the beings that serve as patrons for warlocks are not gods.  A warlock might lead a cult dedicated to a demon prince, an archdevil, or an utterly alien entity - beings not typically served by clerics.  More often, though, the arrangement is similar to that between a master and an apprentice.  The warlock learns and grows in power, at the cost of occasional services performed on the patron's behalf. \nCharisma and Constitution are most important to Warlocks.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.WARLOCK, true);
            };

            wizardBtn.Click += (s, arg) =>
            {
                desc = "Wild and enigmatic, varied in form and function, the power of magic draws students who seek to master its mysteries.  Some aspire to become like the gods, shaping reality itself.  Though the casting of a typical spell requires merely the utterance of a few strange words, fleeting gestures, and sometimes a pinch or clump of exotic materials, these surface components barely hint at the expertise attained after years of apprenticeship and countless hours of study. \nIntelligence and Dexterity are most important to Wizards.";
                classDesc.Text = desc;
                c.SetClass(KnownValues.ClassType.WIZARD, true);
            };

            continueBtn.Click += (s, arg) =>
            {
                if(desc == "")
                {
                    errMsg.Text = "You Must Pick A Hero Type!";
                }
                else
                {
                    var charScreen = new Intent(this, typeof(NewChar2Screen));
                    var gsMsg1 = new GSActivityMessage();
                    gsMsg1.Message = CharacterSheet.GetBytes(c);
                    charScreen.PutExtra(gsMsg1.CharacterMessage, gsMsg1.Message);
                    StartActivity(charScreen);
                }
            };
        }
    }
}