namespace TaskrowSharp
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }

        public Group(int groupID, string name)
        {
            this.GroupID = groupID;
            this.Name = name;
        }

        internal Group(ApiModels.GroupApi groupApi)
        {
            this.GroupID = groupApi.GroupID;
            this.Name = groupApi.GroupName;
        }
    }
}
