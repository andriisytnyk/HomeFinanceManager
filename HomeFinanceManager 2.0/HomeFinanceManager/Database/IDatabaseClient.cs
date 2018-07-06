using HomeFinanceManager.DataModels;
using System.Collections.Generic;

namespace HomeFinanceManager.Database
{
    interface IDatabaseClient
    {
        List<User> SelectAllUsers();

        List<Bill> SelectAllBills();

        List<Account> SelectAllAccounts();

        List<string> SelectAllLogins();

        User SelectUserByLogin(string login);

        double SelectSumByIdFromBills(int idBalance);

        double SelectSumFromAccounts(string name, int idbalance);

        List<Account> SelectAccountByLogin(string login);

        int SelectIdBalanceAtUsers(string login);

        Bill UpdateOrInsertSumAtBills(string login, double sum);

        User UpdateIdBalanceAtUsers(string login, int idbalance);

        Account UpdateSumAtAccounts(string login, string name, double sum);

        double UpdateSumBySummingAtBills(string login, double sum);

        double UpdateSumBySubtractionAtBills(string login, double sum);

        double UpdateSumBySummingAtAccounts(string login, string name, double sum);

        double UpdateSumBySubtractionAtAccounts(string login, string name, double sum);

        int DeleteBill(int idbalance);

        User InsertLoginPassword(string login, string password);

        Account AddAccount(string login, string name);
    }
}
