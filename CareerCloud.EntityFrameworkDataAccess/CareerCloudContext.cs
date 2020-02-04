using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        private string _conStr;
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _conStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            optionsBuilder.UseSqlServer(_conStr);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<ApplicantEducationPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(ae => ae.ApplicantProfile)
                    .WithMany(ap => ap.ApplicantEducations)
                    .HasForeignKey(k => k.Applicant);
            });

            mb.Entity<ApplicantJobApplicationPoco>(poco =>
            {
                poco.Property(aja => aja.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(aja => aja.ApplicantProfile)
                    .WithMany(ap => ap.ApplicantJobApplications)
                    .HasForeignKey(k => k.Applicant);

                poco.HasOne(aja => aja.CompanyJob)
                    .WithMany(cj => cj.ApplicantJobApplications)
                    .HasForeignKey(k => k.Job);
            });

            mb.Entity<ApplicantProfilePoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(ap => ap.SecurityLogin)
                    .WithOne(sl => sl.ApplicantProfile)
                    .HasForeignKey<ApplicantProfilePoco>(k => k.Login);

                poco.HasOne(ap => ap.SystemCountryCode)
                    .WithMany(scc => scc.ApplicantProfiles)
                    .HasForeignKey(k => k.Country);
            });

            mb.Entity<ApplicantResumePoco>(poco =>
            {
                //poco.Property(t => t.TimeStamp).IsRowVersion();

                poco.HasOne(ar => ar.ApplicantProfile)
                    .WithOne(ap => ap.ApplicantResume)
                    .HasForeignKey<ApplicantResumePoco>(k => k.Applicant);
            });

            mb.Entity<ApplicantSkillPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(asp => asp.ApplicantProfile)
                    .WithMany(ap => ap.ApplicantSkills)
                    .HasForeignKey(k => k.Applicant);
            });

            mb.Entity<ApplicantWorkHistoryPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion();

                poco.HasOne(awh => awh.ApplicantProfile)
                    .WithMany(ap => ap.ApplicantWorkHistories)
                    .HasForeignKey(k => k.Applicant);

                poco.HasOne(awh => awh.SystemCountryCode)
                    .WithMany(scc => scc.ApplicantWorkHistories)
                    .HasForeignKey(k => k.CountryCode);
            });

            mb.Entity<CompanyDescriptionPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(cd => cd.CompanyProfile)
                    .WithMany(cp => cp.CompanyDescriptions)
                    .HasForeignKey(k => k.Company);

                poco.HasOne(cd => cd.SystemLanguageCode)
                    .WithMany(slc => slc.CompanyDescriptions)
                    .HasForeignKey(k => k.LanguageId);
            });

            mb.Entity<CompanyJobDescriptionPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(cjd => cjd.CompanyJob)
                    .WithOne(cj => cj.CompanyJobDescription)
                    .HasForeignKey<CompanyJobDescriptionPoco>(k => k.Job);
            });

            mb.Entity<CompanyJobEducationPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(ae => ae.CompanyJob)
                    .WithOne(ap => ap.CompanyJobEducation)
                    .HasForeignKey<CompanyJobEducationPoco>(k => k.Job);
            });

            mb.Entity<CompanyJobPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(cj => cj.CompanyProfile)
                    .WithMany(cp => cp.CompanyJobs)
                    .HasForeignKey(k => k.Company);
            });

            mb.Entity<CompanyJobSkillPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(cjs => cjs.CompanyJob)
                    .WithOne(cj => cj.CompanyJobSkill)
                    .HasForeignKey<CompanyJobSkillPoco>(k => k.Job);
            });

            mb.Entity<CompanyLocationPoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(cl => cl.CompanyProfile)
                    .WithMany(cp => cp.CompanyLocations)
                    .HasForeignKey(k => k.Company);
            });

            mb.Entity<SecurityLoginsLogPoco>(poco =>
            {
                //poco.Property(t => t.TimeStamp).IsRowVersion();

                poco.HasOne(sll => sll.SecurityLogin)
                    .WithMany(sl => sl.SecurityLoginsLogs)
                    .HasForeignKey(k => k.Login);
            });

            mb.Entity<SecurityLoginsRolePoco>(poco =>
            {
                poco.Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();

                poco.HasOne(slr => slr.SecurityLogin)
                    .WithOne(sl => sl.SecurityLoginsRole)
                    .HasForeignKey<SecurityLoginsRolePoco>(k => k.Login);

                poco.HasOne(slr => slr.SecurityRole)
                    .WithMany(sr => sr.SecurityLoginsRoles)
                    .HasForeignKey(k => k.Role);
            });

            mb.Entity<CompanyProfilePoco>().Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();
            mb.Entity<SecurityLoginPoco>().Property(t => t.TimeStamp).IsRowVersion().IsConcurrencyToken();



            base.OnModelCreating(mb);
        }
    }
}
