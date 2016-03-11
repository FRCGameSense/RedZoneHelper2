using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedZoneHelper2
{
    public partial class SettingsForm : Form
    {

        FRCApi frcApi = new FRCApi();
        TBAApi tbaApi = new TBAApi();
        List<FRCApi.Event> events = new List<FRCApi.Event>();

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.eventCodesString = eventCodesBox.Text.ToUpper();
            Properties.Settings.Default.selectedYear = Convert.ToInt16(yearBox.Value);
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            eventCodesBox.Text = Properties.Settings.Default.eventCodesString;
            yearBox.Value = Properties.Settings.Default.selectedYear;
            events = HelperDataStructures.ReadObjectFromFile<List<FRCApi.Event>>("events.bin");
            events = events.OrderBy(o=>o.dateStart).ToList();
            var autoComplete = new AutoCompleteStringCollection();
            string[] eventNames = this.events.Select(x => x.name).ToArray();
            autoComplete.AddRange(eventNames);
            /*
            foreach (string s in eventNames)
            {
                Console.WriteLine("Adding autocomplete: " + s);
            }
             * */
            eventNameBox.AutoCompleteCustomSource = autoComplete;
        }

        private void refreshEventsListButton_Click(object sender, EventArgs e)
        {
            //get all events for the year
            int year = Properties.Settings.Default.selectedYear;
            events = frcApi.getEvents(year);
            HelperDataStructures.WriteObjectToFile<List<FRCApi.Event>>("frcEvents.bin", events);
        }

        private void refreshSeasonDataButton_Click(object sender, EventArgs e)
        {
            //get all events for the year
            int year = Properties.Settings.Default.selectedYear;
            events = frcApi.getEvents(year, !forceFullUpdateCheckBox.Checked);
            HelperDataStructures.WriteObjectToFile<List<FRCApi.Event>>("frcEvents.bin", events);

            List<HelperDataStructures.MatchData2016> seasonMatches = new List<HelperDataStructures.MatchData2016>();

            foreach (FRCApi.Event evt in events)
            {
                if (evt.code.ToLower() == "scmb")
                {
                    Console.WriteLine("Bleh");
                }
                List<FRCApi.MatchResult> matchResults = frcApi.getMatchResults<FRCApi.MatchResult>(year, evt.code, !forceFullUpdateCheckBox.Checked);
                if (matchResults != null && matchResults.Count > 0)
                {
                    List<FRCApi.ScoreDetails2016> matchScoreDetails = frcApi.getScoreDetails<FRCApi.ScoreDetails2016>(year, evt.code, FRCApi.QualificationMatchesString, !forceFullUpdateCheckBox.Checked);
                    List<FRCApi.ScoreDetails2016> playoffScoreDetails = frcApi.getScoreDetails<FRCApi.ScoreDetails2016>(year, evt.code, FRCApi.PlayoffMatchesString, !forceFullUpdateCheckBox.Checked);

                    if (playoffScoreDetails != null)
                    {
                        matchScoreDetails.AddRange(playoffScoreDetails);
                    }

                    if (matchResults != null && matchScoreDetails != null)
                    {

                        for (int i = 0; i < matchResults.Count; i++)
                        {
                            HelperDataStructures.MatchData2016 matchData = new HelperDataStructures.MatchData2016(matchResults[i], matchScoreDetails[i]);
                            seasonMatches.Add(matchData);
                        }
                    }
                }

            }

            HelperDataStructures.WriteObjectToFile<List<HelperDataStructures.MatchData2016>>("seasonMatchData.bin", seasonMatches);
            HelperDataStructures.WriteObjectToFile<List<FRCApi.Event>>("events.bin", events);

        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            string currentText = eventCodesBox.Text.Replace(" ", "");
            string eventCodeToAdd = events.Find(i => i.name.ToLower() == eventNameBox.Text.ToLower()).code;

            if (currentText.Length > 0)
            {
                eventCodesBox.Text = currentText + "," + eventCodeToAdd;
            }
            else
            {
                eventCodesBox.Text = eventCodeToAdd;
            }

            eventNameBox.Clear();
        }
    }
}
