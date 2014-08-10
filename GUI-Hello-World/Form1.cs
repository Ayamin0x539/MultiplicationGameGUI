using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Hello_World
{
    public partial class Form1 : Form
    {
        private String szInput = "null"; // for storing input
        private int level = 1;
        // All games will start out with "What is 1 x 1?"
        private int multiplicand = 1;
        private int multiplier = 1;
        private int product = 1;
        private int expOverflow = 0;
        // We will randomly generate the multiplicand and multiplier.
        private Random rng = new Random();
        // Flag keeping track of whether the player is correct or not.
        private bool fCorrect;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                // Compute the product, convert to string to compare with user's input in the TextBox.
                product = multiplicand * multiplier;
                fCorrect = (product.ToString() == szInput);
                // Case: User has not entered anything in the box
                if (szInput == "null" || szInput == "")
                {
                    MessageBox.Show("Please type your answer in the text box.");
                }
                else if (fCorrect)
                {
                    // Experience gained will scale with the difficulty of the question: exp gained will be 10 times the product.
                    // Check to see if we have excess experience that goes past the maximum:
                    if (this.progressBar1.Value + product * 10 >= this.progressBar1.Maximum)
                    {
                        // If so, calculate the left over experience:
                        expOverflow = product * 10 - (this.progressBar1.Maximum - this.progressBar1.Value);
                    }
                    this.progressBar1.Increment(product*10); // peform the actual increment
                    MessageBox.Show("Correct! " + multiplicand + " x " + multiplier + " = " + product + "!\nYou gained some experience.");
                    if (this.progressBar1.Value == this.progressBar1.Maximum) // If the experience bar hits the required amount...
                    {
                        this.label2.Text = "Level " + ++level; // ... we'll level up the player...
                        this.progressBar1.Value = 0; // ... reset the experience bar ...
                        this.progressBar1.Maximum = level * level * 50; // ... increase the experience bar to f(x) = 50x^2 so it gets harder to level as level increases ...
                        this.progressBar1.Increment(expOverflow); // ... add in the excess experience from the previous level ...
                        expOverflow = 0; // reset the excess counter, so we don't accidentally add it in unless there is actually an overflow
                        MessageBox.Show("You levelled up! You are now level " + level); // ... and send a message to the player notifying of the level up.
                    }
                    // Randomly generate the new question and update the label: (we will increase the number range once level 7 is reached)
                    if (level < 7)
                    {
                        multiplicand = rng.Next(1, 13);
                        multiplier = rng.Next(1, 13);
                    }
                    else
                    {
                        multiplicand = rng.Next(1, 20);
                        multiplier = rng.Next(1, 20);
                    }
                    this.label1.Text = "What is " + multiplicand + " x " + multiplier + "?";
                }
                else
                {
                    MessageBox.Show("Sorry, " + multiplicand + " x " + multiplier + " is not " + szInput + ".\nTry again!");
                }
            }
            else
            {
                MessageBox.Show("Looks like you left the box unticked! \nYour answer will not be checked unless the box is ticked.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Stores the textbox string into szInput whenever th textbox is updated:
            this.szInput = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Experience: " + this.progressBar1.Value + " out of " + this.progressBar1.Maximum);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
