using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Babysitter
{
    public partial class TnC : Form
    {
        public TnC()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TnC_Load(object sender, EventArgs e)
        {
            
                string termsAndConditions = "Terms and Conditions for Sitters:\r\n\r\n" +
                    "1. Eligibility:\r\n" +
                    "   Sitters must be at least 16 years old and provide valid documentation, including a National ID (NID) or Birth Certificate, for verification.\r\n" +
                    "2. Profile Information:\r\n" +
                    "   Sitters must provide accurate and up-to-date information, including their availability and any relevant skills or interests.\r\n" +
                    "3. Conduct:\r\n" +
                    "   Sitters must behave responsibly, respect the parents’ homes, and ensure the safety and well-being of the children at all times.\r\n" +
                    "   Sitters are not allowed to use the stove for cooking purposes.\r\n" +
                    "   Smoking and inviting guests are strictly prohibited while babysitting.\r\n" +
                    "4. Confidentiality:\r\n" +
                    "   Sitters must respect the privacy of parents and children and must not disclose any personal information about the families they work for.\r\n" +
                    "5. Guardian Contact (for Sitters under 18):\r\n" +
                    "   Sitters under the age of 18 must provide a parent or legal guardian's contact number and ensure it remains valid.\r\n" +
                    "6. Scheduling and Cancellations:\r\n" +
                    "   Sitters must update their availability in the app promptly and inform parents at least 24 hours in advance if they need to cancel a scheduled babysitting session.\r\n" +
                    "7. Prohibited Actions:\r\n" +
                    "   Sitters must not engage in any harmful behavior or neglect their duties.\r\n" +
                    "   Sitters are not obligated to perform any household chores or tasks unrelated to childcare.\r\n" +
                    "8. Service Location:\r\n" +
                    "   Sitters can provide services in a maximum of two preselected locations. They cannot offer services outside the specified areas listed in their profile.\r\n" +
                    "9. Disclaimer:\r\n" +
                    "   The app is not liable for any disputes or incidents that occur between sitters and parents. Both parties are encouraged to communicate clearly to avoid misunderstandings.\r\n" +
                    "Terms and Conditions for Parents:\r\n\r\n" +
                    "1. Eligibility:\r\n" +
                    "   Only parents or legal guardians of children aged 1 to 12 years are eligible to use the app. The service does not accommodate infants (children under 1 year old) due to their sensitivity and specific care needs.\r\n" +
                    "2. Documentation:\r\n" +
                    "   Parents must verify their identity by submitting a valid NID and the Birth Certificate of their children.\r\n" +
                    "3. Profile Information:\r\n" +
                    "   Parents must provide accurate information about the number of children, their ages (1 to 12 years), and any specific needs or instructions for the sitter.\r\n" +
                    "4. Child Safety:\r\n" +
                    "   Parents are responsible for ensuring their children’s safety and must provide accurate emergency contact information.\r\n" +
                    "5. Scheduling and Cancellations:\r\n" +
                    "   Parents must schedule babysitting sessions responsibly and notify sitters promptly in case of any cancellations or changes.\r\n" +
                    "6. Instructions for Sitters:\r\n" +
                    "   Parents must provide clear and concise instructions regarding the children’s care, including feeding, bedtime, and other essential routines.\r\n" +
                    "7. Confidentiality:\r\n" +
                    "   Parents must not share personal information about the sitter outside the app or misuse it in any way.\r\n" +
                    "8. Payment:\r\n" +
                    "   Payments must be made as per the terms agreed with the sitter through the app’s payment feature (if applicable).\r\n" +
                    "9. Prohibited Actions:\r\n" +
                    "   Parents must not make inappropriate requests or engage in behavior that jeopardizes the sitter’s safety or comfort.\r\n" +
                    "10. Legal Compliance:\r\n" +
                    "   Parents must adhere to all local childcare regulations and ensure that their actions comply with the law.\r\n" +
                    "11. Disclaimer:\r\n" +
                    "   The app serves as a platform to connect parents and sitters and is not liable for any disputes, injuries, or other incidents arising from its use.\r\n" +
                    "General Terms (Applicable to Both):\r\n\r\n" +
                    "1. Account Responsibility:\r\n" +
                    "   Both sitters and parents are responsible for maintaining the confidentiality of their accounts and ensuring no unauthorized access.\r\n" +
                    "2. Termination:\r\n" +
                    "   The app reserves the right to suspend or terminate any account that violates these terms and conditions.\r\n" +
                    "3. Dispute Resolution:\r\n" +
                    "   In case of any disputes, both parties agree to communicate and resolve the matter directly. The app provides no mediation services.\r\n" +
                    "4. Changes to Terms:\r\n" +
                    "   These terms may be updated periodically. Users will be notified of any significant changes.\r\n" +
                    "5. Liability:\r\n" +
                    "   The app is not responsible for any damages, injuries, or disputes arising from the interactions between parents and sitters.";

                richTextBox1.Text = termsAndConditions;
                richTextBox1.ReadOnly = true;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            

        }


        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    }
    
