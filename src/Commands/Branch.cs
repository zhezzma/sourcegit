﻿namespace SourceGit.Commands
{
    public static class Branch
    {
        public static string ShowCurrent(string repo)
        {
            var cmd = new Command();
            cmd.WorkingDirectory = repo;
            cmd.Context = repo;
            cmd.Args = $"branch --show-current";
            return cmd.ReadToEnd().StdOut.Trim();
        }

        public static bool Create(string repo, string name, string basedOn)
        {
            var cmd = new Command();
            cmd.WorkingDirectory = repo;
            cmd.Context = repo;
            cmd.Args = $"branch {name} {basedOn}";
            return cmd.Exec();
        }

        public static bool Rename(string repo, string name, string to)
        {
            var cmd = new Command();
            cmd.WorkingDirectory = repo;
            cmd.Context = repo;
            cmd.Args = $"branch -M {name} {to}";
            return cmd.Exec();
        }

        public static bool SetUpstream(string repo, string name, string upstream)
        {
            var cmd = new Command();
            cmd.WorkingDirectory = repo;
            cmd.Context = repo;

            if (string.IsNullOrEmpty(upstream))
                cmd.Args = $"branch {name} --unset-upstream";
            else
                cmd.Args = $"branch {name} -u {upstream}";

            return cmd.Exec();
        }

        public static bool DeleteLocal(string repo, string name)
        {
            var cmd = new Command();
            cmd.WorkingDirectory = repo;
            cmd.Context = repo;
            cmd.Args = $"branch -D {name}";
            return cmd.Exec();
        }

        public static bool DeleteRemote(string repo, string remote, string name)
        {
            bool exists = new Remote(repo).HasBranch(remote, name);
            if (exists)
                return new Push(repo, remote, $"refs/heads/{name}", true).Exec();

            var cmd = new Command();
            cmd.WorkingDirectory = repo;
            cmd.Context = repo;
            cmd.Args = $"branch -D -r {remote}/{name}";
            return cmd.Exec();
        }
    }
}
