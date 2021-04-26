using PaperRulez.Interfaces;
using PaperRulez.Services;
using System.IO;
using Xunit;

namespace PaperRulezTests
{
    public class FileReaderTests
    {
        private readonly string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private readonly IFileHelper _fileReader;

        public FileReaderTests()
        {
            _fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\DOCE4878_largeTestFile.text"));
        }

        [Fact]
        public void CanReadFirstLine()
        {
            var expectedFirstLine = "lookup|Cat,owner,box";
            var actualFirstLine = _fileReader.ReadFirstLine();

            Assert.Equal(expectedFirstLine, actualFirstLine);
        }

        [Fact]
        public void CanGetTextBlocks()
        {
            string[] expectedBlocks = new string[2] { @"Cat ipsum dolor sit amet, dont wait for the storm to pass, dance in the rain put butt in owner's face, cat jumps and falls onto the couch purrs and wakes up in a new dimension filled with kitty litter meow meow yummy there is a bunch of cats hanging around eating catnip . Step on your keyboard while you're gaming and then turn in a circle . Sweet beast hide when guests come over, so i love cuddles, cat sit like bread chew iPad power cord, or be superior. A nice warm laptop for me to sit on woops poop hanging from butt must get rid run run around house drag poop on floor maybe it comes off woops left brown marks on floor human slave clean lick butt now yet purr purr purr until owner pets why owner not pet me hiss scratch meow. Am in trouble, roll over, too cute for human to get mad terrorize the hundred-and-twenty-pound rottweiler and steal his bed, not sorry yet stretch out on bed the fat cat sat on the mat bat away with paws. Wack the mini furry mouse time to go zooooom for ignore the squirrels, you'll never catch them anyway so cats woo growl at dogs in my sleep or fart in owners food if it fits i sits. Catasstrophe i like cats because they are fat and fluffy but chirp at birds. Gnaw the corn cob spot something, big eyes, big eyes, crouch, shake butt, prepare to pounce, purr sit in box, oooo! dangly balls! jump swat swing flies so sweetly to the floor crash move on wash belly nap pushes butt to face. Flop over the door is opening! how exciting oh, it's you, meh or mewl for food at 4am, with tail in the air fat baby cat best buddy little guy yet gnaw the corn cob meeeeouw. Asdflkjaertvlkjasntvkjn (sits on keyboard) cat walks in keyboard but cat milk copy park pee walk owner escape bored tired cage droppings sick vet vomit mouse. Meoooow scream for no reason at 4 am Cat ipsum dolor sit amet, dont wait for the storm to pass, dance in the rain put butt in owner's face, cat jumps and falls onto the couch purrs and wakes up in a new dimension filled with kitty litter meow meow yummy there is a bunch of cats hanging around eating catnip . Step on your keyboard while you're gaming and then turn in a circle . Sweet beast hide when guests come over, so i love cuddles, cat sit like bread chew iPad power cord, or be superior. A nice warm laptop for me to sit on woops poop hanging from butt must get rid run run around house drag poop on floor maybe it comes off woops left brown marks on floor human slave clean lick butt now yet purr purr purr until owner pets why owner not pet me hiss scratch meow. Am in trouble, roll over, too cute for human to get mad terrorize the hundred-and-twenty-pound rottweiler and steal his bed, not sorry yet stretch out on bed the fat cat sat on the mat bat away with paws. Wack the mini furry mouse time to go zooooom for ignore the squirrels, you'll never catch them anyway so cats woo growl at dogs in my sleep or fart in owners food if it fits i sits. Catasstrophe i like cats because they are fat and fluffy but chirp at birds. Gnaw the corn cob spot something, big eyes, big eyes, crouch, shake butt, prepare to pounce, purr sit in box, oooo! dangly balls! jump swat swing flies so sweetly to the floor crash move on wash belly nap pushes butt to face. Flop over the door is opening! how exciting oh, it's you, meh or mewl for food at 4am, with tail in the air fat baby cat best buddy little guy yet gnaw the corn cob meeeeouw. Asdflkjaertvlkjasntvkjn (sits on keyboard) cat walks in keyboard but cat milk copy park pee walk owner escape bored tired cage droppings sick vet vomit mouse. Meoooow scream for no reason at 4 am Cat ipsum dolor sit amet, dont wait for the storm to pass, dance in the rain put butt in owner's face, cat jumps and falls onto the couch purrs and wakes up in a new dimension filled with kitty litter meow meow yummy there is a bunch of cats hanging around eating catnip . Step on your keyboard while you're gaming and then turn in a circle . Sweet beast hide when guests come over, so i love cuddles, cat sit like bread chew iPad power cord, or be superior. A nice warm laptop for me to sit on woops p",
@"oop hanging from butt must get rid run run around house drag poop on floor maybe it comes off woops left brown marks on floor human slave clean lick butt now yet purr purr purr until owner pets why owner not pet me hiss scratch meow. Am in trouble, roll over, too cute for human to get mad terrorize the hundred-and-twenty-pound rottweiler and steal his bed, not sorry yet stretch out on bed the fat cat sat on the mat bat away with paws. Wack the mini furry mouse time to go zooooom for ignore the squirrels, you'll never catch them anyway so cats woo growl at dogs in my sleep or fart in owners food if it fits i sits. Catasstrophe i like cats because they are fat and fluffy but chirp at birds. Gnaw the corn cob spot something, big eyes, big eyes, crouch, shake butt, prepare to pounce, purr sit in box, oooo! dangly balls! jump swat swing flies so sweetly to the floor crash move on wash belly nap pushes butt to face. Flop over the door is opening! how exciting oh, it's you, meh or mewl for food at 4am, with tail in the air fat baby cat best buddy little guy yet gnaw the corn cob meeeeouw. Asdflkjaertvlkjasntvkjn (sits on keyboard) cat walks in keyboard but cat milk copy park pee walk owner escape bored tired cage droppings sick vet vomit mouse. Meoooow scream for no reason at 4 am Cat ipsum dolor sit amet, dont wait for the storm to pass, dance in the rain put butt in owner's face, cat jumps and falls onto the couch purrs and wakes up in a new dimension filled with kitty litter meow meow yummy there is a bunch of cats hanging around eating catnip . Step on your keyboard while you're gaming and then turn in a circle . Sweet beast hide when guests come over, so i love cuddles, cat sit like bread chew iPad power cord, or be superior. A nice warm laptop for me to sit on woops poop hanging from butt must get rid run run around house drag poop on floor maybe it comes off woops left brown marks on floor human slave clean lick butt now yet purr purr purr until owner pets why owner not pet me hiss scratch meow. Am in trouble, roll over, too cute for human to get mad terrorize the hundred-and-twenty-pound rottweiler and steal his bed, not sorry yet stretch out on bed the fat cat sat on the mat bat away with paws. Wack the mini furry mouse time to go zooooom for ignore the squirrels, you'll never catch them anyway so cats woo growl at dogs in my sleep or fart in owners food if it fits i sits. Catasstrophe i like cats because they are fat and fluffy but chirp at birds. Gnaw the corn cob spot something, big eyes, big eyes, crouch, shake butt, prepare to pounce, purr sit in box, oooo! dangly balls! jump swat swing flies so sweetly to the floor crash move on wash belly nap pushes butt to face. Flop over the door is opening! how exciting oh, it's you, meh or mewl for food at 4am, with tail in the air fat baby cat best buddy little guy yet gnaw the corn cob meeeeouw. Asdflkjaertvlkjasntvkjn (sits on keyboard) cat walks in keyboard but cat milk copy park pee walk owner escape bored tired cage droppings sick vet vomit mouse. Meoooow scream for no reason at 4 am final" + new string(new char[999]) };

            int i = 0;

            var actualBlocks = new string[2];

            foreach (var actualBlock in _fileReader.ReadBlock())
            {
                actualBlocks[i++] = actualBlock;
            }

            Assert.Equal(expectedBlocks[0], actualBlocks[0]);
            Assert.Equal(expectedBlocks[1], actualBlocks[1]);
        }

        [Fact]
        public void CanReadFileName()
        {
            var expectedFileName = "DOCE4878_largeTestFile.text";
            var actualFileName = _fileReader.FileName();

            Assert.Equal(expectedFileName, actualFileName);
        }
    }
}
