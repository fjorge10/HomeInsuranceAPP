using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class HomeInsuranceRepository : IDisposable
    {
        private HomeInsuranceEntities context;

        public HomeInsuranceRepository()
        {
            this.context = new HomeInsuranceEntities();
        }

        public List<Policy> ListPolicy()
        {
            return this.context.Policies.ToList();
        }

        /// <summary>
        /// Method to add new Policy object to the db context
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        public int CreatePolicy(Policy policy)
        {
            Policy policyAdd = this.context.Policies.Add(policy);

            try
            {
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            return policyAdd.PolicyNumber;
        }

        #region dispose 

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
