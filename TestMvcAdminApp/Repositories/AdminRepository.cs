using TestMvcAdminApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestMvcAdminApp.Repositories {
    public class AdminRepository {

        #region Get All - UserDetails / Roles / Permissions / Rights

        public static List<UserDetailsWithRoleDTO> GetAllUsers() {
            var data = new List<UserDetailsWithRoleDTO>();
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetAllUsers", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {

                        data.Add(new UserDetailsWithRoleDTO() {

                            ID = item["ID"] == DBNull.Value ? 0 : Convert.ToInt32(item["ID"]),
                            FirstName = item["FirstName"] == DBNull.Value ? "" : Convert.ToString(item["FirstName"]),
                            LastName = item["LastName"] == DBNull.Value ? "" : Convert.ToString(item["LastName"]),
                            CompanyName = item["CompanyName"] == DBNull.Value ? "" : Convert.ToString(item["CompanyName"]),
                            Mobile = item["Mobile"] == DBNull.Value ? "" : Convert.ToString(item["Mobile"]),
                            RoleID = item["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(item["RoleID"]),
                            RoleName = item["RoleName"] == DBNull.Value ? "" : Convert.ToString(item["RoleName"]),
                            RoleDescription = item["RoleDescription"] == DBNull.Value ? "" : Convert.ToString(item["RoleDescription"])
                        });
                    }
                }
            }
            return data;
        }

        public static List<RoleWithPermissionDTO> GetAllRoles() {
            var data = new List<RoleWithPermissionDTO>();
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetAllRoles", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {

                        data.Add(new RoleWithPermissionDTO() {
                            ID = item["ID"] == DBNull.Value ? 0 : Convert.ToInt32(item["ID"]),
                            Name = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]),
                            Description = item["Description"] == DBNull.Value ? "" : Convert.ToString(item["Description"]),
                            PermissionID = item["PermissionID"] == DBNull.Value ? 0 : Convert.ToInt32(item["PermissionID"]),
                            PermissionName = item["PermissionName"] == DBNull.Value ? "" : Convert.ToString(item["PermissionName"]),
                            PermissionDescription = item["PermissionDescription"] == DBNull.Value ? "" : Convert.ToString(item["PermissionDescription"]),
                            UsersCount = item["UsersCount"] == DBNull.Value ? 0 : Convert.ToInt32(item["UsersCount"])
                        });
                    }
                }
            }
            return data;

        }

        public static List<PermissionWithRightDTO> GetAllPermissions() {
            var data = new List<PermissionWithRightDTO>();
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetAllPermissions", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {

                        data.Add(new PermissionWithRightDTO() {
                            ID = item["ID"] == DBNull.Value ? 0 : Convert.ToInt32(item["ID"]),
                            Name = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]),
                            Description = item["Description"] == DBNull.Value ? "" : Convert.ToString(item["Description"]),
                            RightID = item["RightID"] == DBNull.Value ? 0 : Convert.ToInt32(item["RightID"]),
                            RightName = item["RightName"] == DBNull.Value ? "" : Convert.ToString(item["RightName"]),
                            RightDescription = item["RightDescription"] == DBNull.Value ? "" : Convert.ToString(item["RightDescription"]),
                            RolesCount = item["RolesCount"] == DBNull.Value ? 0 : Convert.ToInt32(item["RolesCount"])
                        });
                    }
                }
            }
            return data;


        }

        public static List<Right> GetAllRights() {
            var data = new List<Right>();
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetAllRights", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {

                        data.Add(new Right() {
                            Name = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]),
                            Description = item["Description"] == DBNull.Value ? "" : Convert.ToString(item["Description"])
                        });
                    }
                }
            }
            return data;

        }

        public static List<string> GetAllRightIDs() {
            var data = new List<string>();
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetAllRightIDs", con)) {
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        data.Add(item[0] == DBNull.Value ? "" : Convert.ToString(item[0]));
                    }
                }
            }
            return data;
        }

        #endregion

        #region Get By UserID - Roles / Permissions / Rights

        public static List<RolesForUserDTO> GetRolesByUserID(int userID) {
            var data = new List<RolesForUserDTO>();
            var rolesForUserDTO = new RolesForUserDTO();

            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRolesByUserID", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    con.Open();

                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    foreach (DataRow item in ds.Tables[0].Rows) {
                        rolesForUserDTO.UserID = item["UserID"] == DBNull.Value ? 0 : Convert.ToInt32(item["UserID"]);
                        rolesForUserDTO.FirstName = item["FirstName"] == DBNull.Value ? "" : Convert.ToString(item["FirstName"]);
                        rolesForUserDTO.LastName = item["LastName"] == DBNull.Value ? "" : Convert.ToString(item["LastName"]);

                        foreach (DataRow roleItem in ds.Tables[1].Rows) {
                            rolesForUserDTO.RolesWithIsAssigned.Add(new RoleWithIsAssigned() {
                                ID = roleItem["ID"] == DBNull.Value ? 0 : Convert.ToInt32(roleItem["ID"]),
                                Name = roleItem["Name"] == DBNull.Value ? "" : Convert.ToString(roleItem["Name"]),
                                Description = roleItem["Description"] == DBNull.Value ? "" : Convert.ToString(roleItem["Description"]),
                                IsAssigned = roleItem["IsAssigned"] == DBNull.Value ? false : Convert.ToBoolean(roleItem["IsAssigned"])
                            });
                        }
                        data.Add(rolesForUserDTO);
                    };
                }
            }

            return data;
        }

        public static List<PermissionsForRoleDTO> GetPermissionsByRoleID(int roleID) {
            var data = new List<PermissionsForRoleDTO>();
            var permissionsForRoleDTO = new PermissionsForRoleDTO();

            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetPermissionsByRoleID", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    con.Open();

                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        permissionsForRoleDTO.RoleID = item["RoleID"] == DBNull.Value ? 0 : Convert.ToInt32(item["RoleID"]);
                        permissionsForRoleDTO.RoleName = item["RoleName"] == DBNull.Value ? "" : Convert.ToString(item["RoleName"]);
                        permissionsForRoleDTO.RoleDescription = item["RoleDescription"] == DBNull.Value ? "" : Convert.ToString(item["RoleDescription"]);

                        foreach (DataRow permissionItem in ds.Tables[1].Rows) {
                            permissionsForRoleDTO.PermissionsWithIsAssigned.Add(new PermissionWithIsAssigned() {
                                ID = permissionItem["ID"] == DBNull.Value ? 0 : Convert.ToInt32(permissionItem["ID"]),
                                Name = permissionItem["Name"] == DBNull.Value ? "" : Convert.ToString(permissionItem["Name"]),
                                Description = permissionItem["Description"] == DBNull.Value ? "" : Convert.ToString(permissionItem["Description"]),
                                IsAssigned = permissionItem["IsAssigned"] == DBNull.Value ? false : Convert.ToBoolean(permissionItem["IsAssigned"])
                            });
                        }
                        data.Add(permissionsForRoleDTO);
                    }

                }
            }

            return data;
        }

        public static List<RightsForPermissionDTO> GetRightsByPermissionID(int permissionID) {
            var data = new List<RightsForPermissionDTO>();
            var rightsForPermissionDTO = new RightsForPermissionDTO();

            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRightsByPermissionID", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionID", permissionID);
                    con.Open();

                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        rightsForPermissionDTO.PermissionID = item["PermissionID"] == DBNull.Value ? 0 : Convert.ToInt32(item["PermissionID"]);
                        rightsForPermissionDTO.PermissionName = item["PermissionName"] == DBNull.Value ? "" : Convert.ToString(item["PermissionName"]);
                        rightsForPermissionDTO.PermissionDescription = item["PermissionDescription"] == DBNull.Value ? "" : Convert.ToString(item["PermissionDescription"]);

                        foreach (DataRow rightItem in ds.Tables[1].Rows) {
                            rightsForPermissionDTO.RightsWithIsAssigned.Add(new RightWithIsAssigned() {
                                ID = rightItem["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rightItem["ID"]),
                                Name = rightItem["Name"] == DBNull.Value ? "" : Convert.ToString(rightItem["Name"]),
                                Description = rightItem["Description"] == DBNull.Value ? "" : Convert.ToString(rightItem["Description"]),
                                IsAssigned = rightItem["IsAssigned"] == DBNull.Value ? false : Convert.ToBoolean(rightItem["IsAssigned"])
                            });
                        }
                        data.Add(rightsForPermissionDTO);
                    }
                }
            }

            return data;
        }

        public static List<Role> GetRolesHavingPermission(int permissionID) {
            var data = new List<Role>();

            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRolesHavingPermission", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionID", permissionID);
                    con.Open();

                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        data.Add(new Role {
                            ID = item["ID"] == DBNull.Value ? 0 : Convert.ToInt32(item["ID"]),
                            Name = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]),
                            Description = item["Description"] == DBNull.Value ? "" : Convert.ToString(item["Description"])
                        });
                    }
                }
            }
            return data;
        }

        public static List<Right> GetRightsHavingPermissions(string permissionIDs) {
            var data = new List<Right>();
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRightsHavingPermissions", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionIDs", permissionIDs);
                    con.Open();

                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        data.Add(new Right {
                            ID = item["ID"] == DBNull.Value ? 0 : Convert.ToInt32(item["ID"]),
                            Name = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]),
                            Description = item["Description"] == DBNull.Value ? "" : Convert.ToString(item["Description"])
                        });
                    }
                }
            }
            return data;
        }

        #endregion

        #region Create - UserDetails / Role / Permission / Right

        public static int CreateUserDetails(UserDetails model) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("CreateUserDetails", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", model.LastName);
                    cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                    cmd.Parameters.AddWithValue("@CompanyName", model.CompanyName);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        result = reader["UserDetailsID"] != DBNull.Value ? Convert.ToInt32(reader["UserDetailsID"]) : 0;
                    }
                }
            }
            return result;
        }

        public static int CreateRole(Role model) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("CreateRole", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        result = reader["RoleID"] != DBNull.Value ? Convert.ToInt32(reader["RoleID"]) : 0;
                    }

                }
            }
            return result;
        }

        public static int CreatePermission(Permission model) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("CreatePermission", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        result = reader["PermissionID"] != DBNull.Value ? Convert.ToInt32(reader["PermissionID"]) : 0;
                    }

                }
            }
            return result;
        }

        public static int CreateRight(Right model) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("CreateRight", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        result = reader["RightID"] != DBNull.Value ? Convert.ToInt32(reader["RightID"]) : 0;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Assign - Roles / Permissions / Rights (mapping)

        public static int AssignRolesToUser(List<AssignRolesToUser> model) {
            int result;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("AssignRolesToUser", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", model.First().UserID);
                    cmd.Parameters.AddWithValue("@RoleIDs", model.First().RoleIDs);

                    con.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static int AssignPermissionsToRole(List<AssignPermissionsToRole> model) {
            int result;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("AssignPermissionsToRole", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", model.First().RoleID);
                    cmd.Parameters.AddWithValue("@PermissionIDs", model.First().PermissionIDs);
                    con.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static int AssignRightsToPermission(List<AssignRightsToPermission> model) {
            int result;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("AssignRightsToPermission", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionID", model.First().PermissionID);
                    cmd.Parameters.AddWithValue("@RightIDs", model.First().RightIDs);
                    con.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static int AssignRightsToRole(List<AssignRightsToRole> model) {
            int result;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("AssignRightsToRole", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", model.First().RoleID);
                    cmd.Parameters.AddWithValue("@RightIDs", model.First().RightIDs);
                    con.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        #endregion

        #region Get Name by ID

        public static string GetUserNameByUserID(int userID) {
            string userName = "";
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetUserNameByUserID", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        userName = item["UserName"] == DBNull.Value ? "" : Convert.ToString(item["UserName"]);
                    }
                }
            }
            return userName;
        }

        public static string GetRoleNameByRoleID(int roleID) {
            string roleName = "";

            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRoleNameByRoleID", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID);

                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        roleName = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]);
                    }
                }
            }
            return roleName;
        }

        public static string GetRightNameByRightID(int rightID) {

            string rightName = "";

            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRightNameByRightID", con)) {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RightID", rightID);

                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        rightName = item["Name"] == DBNull.Value ? "" : Convert.ToString(item["Name"]);
                    }
                }
            }
            return rightName;
        }

        #endregion

        #region Delete Role / Permission 
        public static int DeleteRole(int roleID) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("DeleteRole", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }

            }
            return result;
        }

        public static int DeletePermission(int permissionID) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("DeletePermission", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionID", permissionID);
                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        #endregion

        public static int GetRolesCountForPermission(int permissionID) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetRolesCountForPermission", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionID", permissionID);
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        result = item[0] == DBNull.Value ? 0 : Convert.ToInt32(item[0]);
                    }
                }
            }
            return result;
        }

        public static int GetUsersCountForRole(int roleID) {
            var result = 0;
            using (SqlConnection con = new SqlConnection(Helper.Connection())) {
                using (SqlCommand cmd = new SqlCommand("GetUsersCountForRole", con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    con.Open();
                    /* Create instance of DataAdapter to read multiple DataSet */
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);

                    /* reading multiple DataSet */
                    foreach (DataRow item in ds.Tables[0].Rows) {
                        result =  item[0] == DBNull.Value ? 0 : Convert.ToInt32(item[0]);
                    }
                }
            }
            return result;
        }
    }
}
