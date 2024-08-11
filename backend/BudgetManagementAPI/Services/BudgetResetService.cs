using BudgetManagementAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Services
{
    public class BudgetResetService
    {
        private readonly UnitOfWork _unitOfWork;

        public BudgetResetService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ResetBudgetSpentAmountsAsync()
        {
            var budgets = await _unitOfWork.Budgets.ToListAsync();

            foreach (var budget in budgets)
            {
                budget.TotalPercentageSpent = 0;
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
