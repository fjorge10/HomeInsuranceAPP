using DataAccessLayer;
using DataAccessLayer.Model;
using HomeInsuranceAPP.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeInsuranceAPP.Models
{
    public class PolicyModel
    {
        [Display(Name = "PolicyNumber",ResourceType = typeof(Resources))]
        public int PolicyNumber { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Resources))]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Resources))]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "ID", ResourceType = typeof(Resources))]
        [Required]
        public int ClientNumber { get; set; }

        #region MyCRUD

        public static List<PolicyModel> List(int clientId)
        {
            List<PolicyModel> policyList = new List<PolicyModel>();

            using (HomeInsuranceRepository repo = new HomeInsuranceRepository())
            {
                List<Policy> policy = repo.ListPolicy().Where(x=>x.ClientNumber == clientId).ToList();

                foreach (Policy item in policy)
                {
                    PolicyModel model = new PolicyModel()
                    {
                        PolicyNumber = item.PolicyNumber,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        ClientNumber = item.ClientNumber
                    };
                    policyList.Add(model);
                }
            }

            return policyList;
        }

        public int Create()
        {
            using (HomeInsuranceRepository repo = new HomeInsuranceRepository())
            {
                Policy policy = new Policy()
                {
                    ClientNumber = this.ClientNumber,
                    StartDate = this.StartDate,
                    EndDate = this.EndDate,
                    Claims = null,
                    Coverables = null,
                    Coverages = null
                };

                return repo.CreatePolicy(policy);
            }
        }
        #endregion
    }
}