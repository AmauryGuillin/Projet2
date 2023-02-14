using Xunit;
using System;
using System.Collections.Generic;
using Projet2.Models;

namespace TestProject1
{
    public class UnitTest1
    {




        [Fact]
        public void EditAdherent()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int id = dal.CreateAdherent(0, 1, new DateTime(2000, 12, 25), 400.80, "justification-OUI", 0, 0, 0);
                //dal.EditAdherent(id, 0, 33, new DateTime(2000, 12, 25), 33.33, "OUI", 0, 0, 0);
            }

            using (Dal dal = new Dal())
            {
                List<Adherent> adherents = dal.GetAdherents();
                Assert.NotNull(adherents);
                Assert.Single(adherents);
                Assert.Equal(33, adherents[0].NumAdherent);
                Assert.Equal(33.33, adherents[0].Contribution);
                //Assert.Equal("OUI", adherents[0].IDDocuments);
            }
        }

        /*[Fact]
        public void AA_Creation_Compte()
        {
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.CreateAccount(1, "Xx_Sasukedu42_xX", "jesuisleplusfortdesninjaparcequejesuissasuke"); ;

                List<Account> comptes = dal.GetListAccount();

                Assert.NotNull(comptes);
                Assert.Single(comptes);
                Assert.Equal(1, comptes[0].Id);
                Assert.Equal("Xx_Sasukedu42_xX", comptes[0].Username);
                Assert.Equal("jesuisleplusfortdesninjaparcequejesuissasuke", comptes[0].Password);
            }
        }

        [Fact]
        public void AB_Creation_Benevole()
        {
            using (Dal dal = new Dal())
            {
                dal.CreateBenevole(1, 1, 15);

                List<Benevole> benevoles = dal.GetBenevoles();

                Assert.NotNull(benevoles);
                Assert.Single(benevoles);
                Assert.Equal(1, benevoles[0].Id);
                Assert.Equal(1, benevoles[0].AccountId);
                Assert.Equal(15, benevoles[0].NbActionVolunteering);
            }
        }

        [Fact]
        public void AC_Creation_Adherent()
        {
            using (Dal dal = new Dal())
            {

                dal.CreateAdherent(1, 1, 111111, new DateTime(2000, 12, 25), 1000.01, "justification-OUI");

                List<Adherent> adherents = dal.GetAdherents();

                Assert.NotNull(adherents);
                Assert.Single(adherents);
                Assert.Equal(1, adherents[0].Id);
                Assert.Equal(1, adherents[0].BenevoleId);
                Assert.Equal(111111, adherents[0].NumAdherent);
                Assert.Equal(new DateTime(2000, 12, 25), adherents[0].InscriptionDate);
                Assert.Equal("justification-OUI", adherents[0].IDDocuments);

            }
        }*/


    }
}
